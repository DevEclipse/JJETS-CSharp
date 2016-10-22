using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using JJETS.Pos.UI.WPF;

namespace JJETS.Pos.Logic
{
    using Models;
    using System.Data.Entity;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    public static class POS
    {
        public static Context Context = App.Context;

        #region Create Methods
        public static T CreateUser<T>(string name, string email, string password) where T : User, new()
        {

            var checkUser = GetUser<T>(email);
            if (checkUser == null)
            {
                var temp = new T { Name = name, Email = email, Password = password };
                Context.Entry(temp).State = EntityState.Added;
                return temp;
            }
            else
            {
                return checkUser;
            }

        }

        public static T CreateTerm<T>(string name, string createdBy = "Admin") where T : Base, new()
        {
            var temp = new T { Name = name, CreatedBy = createdBy };
            Context.Entry(temp).State = EntityState.Added;
            return temp;
        }

        public static Store CreateStore(this Manager manager, string name, Location location = null)
        {
            var temp = new Store { Name = name, Manager = manager, Location = location, CreatedBy = manager.Name };
            Context.Entry(temp).State = EntityState.Added;
            return temp;
        }


        public static Stock CreateStock(this Store store, Item item, string name, int quantity, Category category = null, Supplier supplier = null)
        {
            var temp = new Stock {
                Name = name,
                Quantity = quantity,
                Item = item,
                Store = store,CreatedBy = store.Manager.Name };
            Context.Entry(temp).State = EntityState.Added;
            return temp;
        }

        public static Transaction CreateTransaction(this Employee employee, Customer customer, Store store, double amount, double discount, StatusTransaction statusTransaction)
        {
            var temp = new Transaction { Employee = employee, Store = store, Customer = customer, Amount = amount, Status = statusTransaction, CreatedBy = employee.Name };
            temp.Total = temp.TransactionItems.Sum(t => t.Stock.Item.RetailPrice);
            temp.Tax = temp.Total * temp.Store.TaxRate;
            temp.Discount = (temp.Total * temp.Store.DiscountRate) + discount;
            temp.Change = temp.Amount - (temp.Total - temp.Discount);
            Context.Entry(temp).State = EntityState.Added;
            return temp;
        }

        public static Transaction CreateTransaction(this Customer customer, Employee employee, Store store, double amount)
        {
            var temp = new Transaction { Employee = employee, Store = store, Customer = customer, Amount = amount, Status = StatusTransaction.Pending, CreatedBy = customer.Name };
            temp.Total = temp.TransactionItems.Sum(t => t.Stock.Item.RetailPrice);
            temp.Tax = temp.Total * temp.Store.TaxRate;
            temp.Discount = (temp.Total * temp.Store.DiscountRate);
            temp.Change = temp.Amount - (temp.Total - temp.Discount);
            Context.Entry(temp).State = EntityState.Added;
            return temp;
        }

        public static TransactionItem CreateTransactionItem(this Transaction transaction, Stock stock, int quantity)
        {
            var temp = new TransactionItem { Stock = stock, Quantity = quantity, Transaction = transaction };
            Context.Entry(temp).State = EntityState.Added;
            return temp;
        }

        public static Item CreateItem(string name, double costPrice, double retailPrice,Category category,Supplier supplier, byte[] image = null)
        {
            var temp = new Item { Name = name, CostPrice = costPrice, RetailPrice = retailPrice, Supplier = supplier, Category = category, Image = image };
            Context.Entry(temp).State = EntityState.Added;
            return temp;
        }


        #endregion

        #region Get Methods
        public static T GetUser<T>(string email) where T : User
        {
            if (!(ValidateEmail(email))) return default(T);
            return Context.Users.SingleOrDefault(user => user.Email == email) as T;
        }

        public static Item GetItem(string name)
        {
            return Context.Items.SingleOrDefault(item => item.Name == name);
        }
        #endregion

        #region Email Methods
        public static void SendEmail(string senderEmail, string password, string subject, string body, string targetEmail)
        {
            try
            {
                var client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 465;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, password);
                client.Timeout = 30000;

                var mail = new MailMessage(senderEmail, targetEmail, subject, body);
                client.Send(mail);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool ValidateEmail(string email)
        {
            try
            {
                var temp = new MailAddress(email);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
        }
        #endregion

        #region Logical Methods
        public static string LoginCheck(string username, string password)
        {
            var tempUser = GetUser<User>(username);
            if (tempUser != null)
            {
                if (tempUser is Admin && ((Admin)tempUser).SecretPassword == password)
                {
                    return "Welcome Admin";
                }
                if (tempUser.PenaltyTime < DateTime.Now || tempUser is Admin)
                {
                    if (tempUser.Retries > 0 || tempUser is Admin)
                    {
                        if (tempUser.Password == password)
                        {
                            if (tempUser.Status != StatusUser.Online || tempUser.Status != StatusUser.Blocked)
                            {
                                tempUser.Status = StatusUser.Online;
                                Context.SaveChanges();
                                return "Login Successful";
                            }
                            else
                            {
                                return $"Account : {tempUser.Name} is online or blocked by the admin";
                            }
                        }
                        else if (!(tempUser is Admin))
                        {
                            //ToastNotification.Show(this,$"Invalid Password, Retries Left {--tempUser.Retries}");
                            Context.SaveChanges();
                        }
                    }
                    else
                    {
                        tempUser.Retries = 3;
                        tempUser.PenaltyTime = DateTime.Now.AddSeconds(60);
                        Context.SaveChanges();
                        return $"Account: {tempUser.Name} is blocked by the system wait for {(int)(tempUser.PenaltyTime - DateTime.Now).TotalSeconds}";
                    }
                }
                else
                {

                    return $"Account: {tempUser.Name} is blocked by the system wait for {(int)(tempUser.PenaltyTime - DateTime.Now).TotalSeconds}";
                }
            }
            else
            {
                return $"Account : {username} doesn't exist register first";
            }
            return "";
        }
        #endregion
    }

}
