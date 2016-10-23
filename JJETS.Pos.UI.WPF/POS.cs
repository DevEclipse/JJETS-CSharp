using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        public static T Save<T>(this T obj, EntityState entState = EntityState.Added) where T : class
        {
            Context.Entry(obj).State = entState;
            Context.SaveChangesAsync();
            return obj;
        }

        public static T CreateUser<T>(string name, string email, string password) where T : User, new()
        {
            return GetUser<T>(email) ?? new T {Name = name, Email = email, Password = password}.Save();
        }

        public static T CreateTerm<T>(this User user,string name, string createdBy = "Admin") where T : Base, new()
        {
            return new T {Name = name, CreatedBy = user }.Save();
        }

        public static Store CreateStore(this Manager manager, string name, Location location = null)
        {
            return new Store {Name = name, Manager = manager, Location = location, CreatedBy = manager}.Save();
        }


        public static Stock CreateStock(this Store store, Item item, string name, int quantity, Category category = null,
            Supplier supplier = null)
        {
            return new Stock
            {
                Name = name,
                Quantity = quantity,
                Item = item,
                Store = store,
                CreatedBy = store.Manager
            }.Save();
        }

        public static Transaction CreateTransaction(this Employee employee, Customer customer, Store store,
            double amount, double discount, StatusTransaction statusTransaction)
        {
            var temp = new Transaction
            {
                Employee = employee,
                Store = store,
                Customer = customer,
                Amount = amount,
                Status = statusTransaction,
                CreatedBy = employee
            };
            temp.Total = temp.TransactionItems.Sum(t => t.Stock.Item.RetailPrice);
            temp.Tax = temp.Total*temp.Store.TaxRate;
            temp.Discount = (temp.Total*temp.Store.DiscountRate) + discount;
            temp.Change = temp.Amount - (temp.Total - temp.Discount);
            return temp.Save();
        }

        public static Transaction CreateTransaction(this Customer customer, Employee employee, Store store,
            double amount)
        {
            var temp = new Transaction
            {
                Employee = employee,
                Store = store,
                Customer = customer,
                Amount = amount,
                Status = StatusTransaction.Pending,
                CreatedBy = customer
            };
            temp.Total = temp.TransactionItems.Sum(t => t.Stock.Item.RetailPrice);
            temp.Tax = temp.Total*temp.Store.TaxRate;
            temp.Discount = (temp.Total*temp.Store.DiscountRate);
            temp.Change = temp.Amount - (temp.Total - temp.Discount);
            return temp.Save();
        }

        public static TransactionItem CreateTransactionItem(this Transaction transaction, Stock stock, int quantity)
        {
            return new TransactionItem {Stock = stock, Quantity = quantity, Transaction = transaction};
        }

        public static Item CreateItem(this User user,string name, double costPrice, double retailPrice, Category category,
            Supplier supplier, byte[] image = null)
        {
            return
                new Item
                {
                    Name = name,
                    CostPrice = costPrice,
                    RetailPrice = retailPrice,
                    Supplier = supplier,
                    Category = category,
                    Image = image
                }.Save();
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

        public static void SendEmail(string senderEmail, string password, string subject, string body,
            string targetEmail)
        {
            try
            {
                var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 465,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, password),
                    Timeout = 30000
                };

                var mail = new MailMessage(senderEmail, targetEmail, subject, body);
                client.Send(mail);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static bool ValidateEmail(string email)
        {
            try
            {
                if (email == "") return false;
                var temp = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        #endregion

        #region Logical Methods

        public static string LoginCheck(string email, string password, string role)
        {
            if (!ValidateEmail(email))
            {
                return "Email format is Invalid";
            }
            var tempUser = GetUser<User>(email);

            if (tempUser != null)
            {
                if (tempUser is Admin && (tempUser as Admin).SecretPassword == password)
                {
                    return "Welcome Admin";
                }
                if (ObjectContext.GetObjectType(tempUser.GetType()).Name != role)
                {
                    return
                        $"Account: {email} role is {ObjectContext.GetObjectType(tempUser.GetType()).Name} you must select {role} in the Role Selection Window to login";
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
                            --tempUser.Retries;
                            Context.SaveChanges();
                            return $"Invalid Password, Retries Left {tempUser.Retries}";

                        }
                    }
                    else
                    {
                        tempUser.Retries = 3;
                        tempUser.PenaltyTime = DateTime.Now.AddSeconds(60);
                        Context.SaveChanges();
                        return
                            $"Account: {tempUser.Name} is blocked by the system wait for {(int) (tempUser.PenaltyTime - DateTime.Now).TotalSeconds}";
                    }
                }
                else
                {

                    return
                        $"Account: {tempUser.Name} is blocked by the system wait for {(int) (tempUser.PenaltyTime - DateTime.Now).TotalSeconds}";
                }
            }
            else
            {
                return $"Account : {email} doesn't exist register first";
            }
            return "";
        }

        public static string RegisterCheck(string name, string email, string password, string role)
        {
            var registerUser = POS.GetUser<User>(email);

            if (email == null)
            {
                return "Enter Email";

            }
            if (registerUser != null)
            {
                return $"Account {email} is already registered";
            }

            if (ValidateEmail(email))
            {
                switch (role)
                {
                    case "Manager":
                        CreateUser<Manager>(name, email, password);
                        break;
                    case "Employee":
                        CreateUser<Employee>(name, email, password);
                        break;
                    case "Customer":
                        CreateUser<Customer>(name, email, password);
                        break;
                }
                return $"Account: {email} with name: {name} is registered as {role} you may now log in";
            }
            else
            {
                return "Email format is Invalid";
            }

        }
        public static ImageSource BytesToImageSource(this byte[] imageData)
        {
            var biImg = new BitmapImage();
            var ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            var imgSrc = (ImageSource)biImg;

            return imgSrc;
        }
        public static byte[] ImageSourceToBytes<T>(this ImageSource imageSource) where T : BitmapEncoder, new()
        {
            byte[] bytes;
            var bitmapSource = imageSource as BitmapSource;
            var encoder = new T();
            if (bitmapSource == null) return null;
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                bytes = stream.ToArray();
            }

            return bytes;
        }
        #endregion
    }
}


