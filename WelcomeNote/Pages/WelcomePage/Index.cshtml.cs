using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WelcomeNote.Models;

namespace WelcomeNote.Pages.WelcomePage
{
    public class IndexModel : PageModel
    {

        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public Welcome welcomeNote { get; set; }
        public IActionResult OnGet()
        {
            DateTime dd = DateTime.Now;
            string date = dd.ToString("dd/MM/yyyy  HH:mm");
            Console.WriteLine(date);
            welcomeNote = new Welcome();
            welcomeNote.DateUpdate = date;

            for (int i = 1; i < 4; i++) //there are 3 files
            {
                string WelcomeFile = "Welcome_para" + i + ".txt";
                var FileLocation = Path.Combine(_env.WebRootPath, "Welcome Files", WelcomeFile);
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(FileLocation))
                    {
                        // Read the stream to a string, and write the string to the console.
                        String line = sr.ReadToEnd();
                        //Console.WriteLine(line);

                        if (i==1)
                        {
                            welcomeNote.Message1 = line;
                            Console.WriteLine("The current text before update is : " + welcomeNote.Message1);
                        }
                        else if (i==2)
                        {
                            welcomeNote.Message2 = line;
                            Console.WriteLine("The current text before update is : " + welcomeNote.Message2);
                        }
                        else
                        {
                            welcomeNote.Message3 = line;
                            Console.WriteLine("The current text before update is : " + welcomeNote.Message3);
                        }
                        
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

           
            return Page();
        }

        public IActionResult OnPost()
        {
            string Filenames = "";
           
            var FileLocation = Path.Combine(_env.WebRootPath);
            for (int i=1; i <4; i++)
            {
                // Set a variable to the Documents path.
                string WelcomeFile = "Welcome_para"+i+".txt";
                FileLocation = Path.Combine(_env.WebRootPath, "Welcome Files", WelcomeFile);

                using (StreamWriter sw = new StreamWriter(FileLocation))
                {
                    if (i==1)
                        sw.WriteAsync(welcomeNote.Message1);
                        Filenames += WelcomeFile+",";

                    if (i==2)
                        sw.WriteAsync(welcomeNote.Message2);
                        Filenames += WelcomeFile + ",";

                    if (i == 3)
                        sw.WriteAsync(welcomeNote.Message3);
                        Filenames += WelcomeFile + ",";
                }
                
            }

            Console.WriteLine("Date : "+welcomeNote.DateUpdate);
            Console.WriteLine("User : " + welcomeNote.User);
            Console.WriteLine("File name : " + Filenames);
            //Console.WriteLine("Paths : " + FileLocation);

            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\zairu\source\repos\WelcomeNote\WelcomeNote\Data\WelcomeNote.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using(SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "INSERT INTO WelcomeNote (Date, UserName, FileName, Directory) VALUES (@DateUpdate, @User, @FName, @Fpath)";

                command.Parameters.AddWithValue("@DateUpdate", welcomeNote.DateUpdate);
                command.Parameters.AddWithValue("@User", welcomeNote.User);
                command.Parameters.AddWithValue("@FName", Filenames);
                command.Parameters.AddWithValue("@Fpath", FileLocation);

                command.ExecuteNonQuery();

            }


            return RedirectToPage("/Index");
        }
    }
}
