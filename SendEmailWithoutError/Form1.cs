using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace SendEmailWithoutError
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        NetworkCredential Nw;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = Emaill.Text;
            string pw = Pw.Text;
            bool exist = InputEmailIsCorrect(email);
            if (!exist)
            {
                MessageBox.Show("Invalid Email Re-Enter Your Email");
            }
            else
            {
                LoginToGmail(email,pw);
                EnableInput();
            }

        }
        private bool InputEmailIsCorrect(string email)
        {
            try
            {
                if (Emaill.Text.Contains("@gmail.com") || Emaill.Text.Contains("@Gmail.com"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        private NetworkCredential LoginToGmail(string emaill,string pw)
        {
            Nw = new NetworkCredential(emaill,pw);
            return Nw;
        }
        private void EnableInput()
        {
            To.ReadOnly = false;
            Subject.ReadOnly = false;
            Body.ReadOnly = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
           try
           {
                string[] email = To.Text.Split(',');
                string[] attachments = AttachFile.Text.Split(',');
                for (int i=0;i<email.Length;i++) {
                    MailMessage msg = new MailMessage(Emaill.Text, email[i], Subject.Text, Body.Text);
                    for (int x=0;x<attachments.Length;x++)
                    {
                        msg.Attachments.Add(new Attachment(attachments[x]));
                    }
                    SmtpClient sm = new SmtpClient("smtp.gmail.com");
                    sm.UseDefaultCredentials = false;
                    sm.Credentials = LoginToGmail(Emaill.Text, Pw.Text);
                    sm.EnableSsl = true;
                    sm.Port = 587;//Gmail Smtp port 465 or 587
                    sm.Send(msg); }
                MessageBox.Show("Message Sent !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send the Email, Please Check Following:"+"\n \n"+"1.Google Security Alert"+"\n"+"2.Gmail Password");
            }
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog of = new OpenFileDialog();
                if (DialogResult.OK == of.ShowDialog()) {
                    string path = of.FileName;
                    string[] lines = System.IO.File.ReadAllLines(path);
                    string email = lines[0];
                    for (int i = 1; i < lines.Length; i++)
                    {
                        email = email + "," + lines[i];
                    }
                    To.Text = email; }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog Of = new OpenFileDialog();
                if (DialogResult.OK == Of.ShowDialog() && button4.Text.Equals("Add Attachment"))
                {
                    AttachFile.Text = Of.FileName;
                    button6.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AttachFile.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string CurrentSelectedFiles = AttachFile.Text;
                OpenFileDialog Of = new OpenFileDialog();
                if (DialogResult.OK == Of.ShowDialog())
                {
                    CurrentSelectedFiles = CurrentSelectedFiles + "," + Of.FileName;
                    AttachFile.Text = CurrentSelectedFiles;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
