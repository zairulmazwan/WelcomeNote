using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WelcomeNote.Models;

namespace WelcomeNote.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _env;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }


        [BindProperty]
        public Welcome welcomeNote { get; set; }


        public void OnGet()
        {
            welcomeNote = new Welcome();
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

                        if (i == 1)
                        {
                            welcomeNote.Message1 = line;
                            Console.WriteLine("The current text before update is : " + welcomeNote.Message1);
                        }
                        else if (i == 2)
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

        }
    }
}
