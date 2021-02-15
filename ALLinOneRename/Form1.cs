using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ALLinOneRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnRenameVOne_Click(object sender, EventArgs e)
        {

        }

        private void BtnRenameVTwo_Click(object sender, EventArgs e)
        {
            int[] numFilter = { 15 };
            bool needFilter = false;

            string usersPath = TbxPath.Text;

            //get the directory info files and check if there is a path
            DirectoryInfo directoryInfo = new DirectoryInfo(usersPath);
            FileInfo[] infos = directoryInfo.GetFiles();
            
            foreach (FileInfo fileInfo in infos)
            {
                try
                {
                    if (fileInfo.Name == "desktop.ini" || fileInfo.Name == "icon.ico")
                        goto END;

                    string filterNumbers = null; // numbers that will be filtered

                    string fileType;

                    string numberSide = "";

                    string seriesName = fileInfo.Directory.Parent.Name;
                    string newPath;
                    string finalName;
                    string seasonNum;

                    int numberFromTheString;
                    fileType = '.' + fileInfo.Name.Split('.')[fileInfo.Name.Split('.').Length - 1];

                    string seasonWithNumber = fileInfo.Directory.Name;
                    string[] seasonAndNumberSplited = seasonWithNumber.Split(' ');

                    if (seasonAndNumberSplited[0].ToLower() == "season")
                    {
                        numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileType, numberSide);
                        seasonNum = seasonAndNumberSplited[1];

                        finalName = CreateFinalName(numberFromTheString, seasonNum, seriesName);

                        File.Move(fileInfo.FullName, usersPath + finalName + fileType);

                        RtbRenamedText.Text += numberFromTheString.ToString() + " Complete \\\\ " + fileInfo.Name + Environment.NewLine;
                    }

                    else
                    {
                        numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileType, numberSide);
                    }
                }
                catch (IOException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(fileInfo.Name + " Already exist");
                    Console.ResetColor();
                }
            END:;
            }

            string CreateFinalName(int numberFromTheString, string seasonNum, string seriesName)
            {
                string helperName;
                string episodeHelper = "E";

                if (numberFromTheString / 10 < 1)
                    episodeHelper = "E0";

                helperName = CreateHelperName(seasonNum, numberFromTheString, episodeHelper);

                return seriesName + " - " + helperName;
            }

            string CreateHelperName(string seasonNum, int numberFromTheString, string episodeHelper)
            {
                if (seasonNum == "00" || seasonNum.ToLower() == "specials")
                    return "S" + seasonNum + episodeHelper + numberFromTheString.ToString();
                else if (Convert.ToInt32(seasonNum) < 9)
                    return "S0" + seasonNum + episodeHelper + numberFromTheString.ToString();
                else
                    return "S" + seasonNum + episodeHelper + numberFromTheString.ToString();
            }

            int GetNumberOutOfString(string File_name, string file_type, string Side)
            {
                // j is current index of the file_name 
                int converted = 0;
                //if we find a number that is episode then i++ happen so we save the episode number and 
                //on the next run when it find a season number or resoulution number it will go to 0 on the next int not on the
                //episode number itself
                int numbers_together = 0;
                //when he find number he start to count so that it won't check if statment IF he is not at least 1 number
                int number_holder = -1;
                //hold a number if it's the only number then 
                string numbers = null;
                for (int j = 0; j < File_name.Length; j++)
                {
                    switch (File_name[j])
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            numbers_together++;
                            numbers += File_name[j];
                            //start recording the numbers if they are found
                            break;
                        default:
                            if (numbers_together != 0)
                            {
                                if (Side == "f")
                                    return Convert.ToInt32(numbers);
                                if (numbers + file_type == File_name)
                                {
                                    converted = Convert.ToInt32(numbers);
                                    number_holder = 0;
                                    goto END;
                                    //if file is just a number then returns that number
                                }
                                if (numbers == "0")
                                    number_holder = 0;
                                if (needFilter)
                                {
                                    for (int i = 0; i < numFilter.Length; i++)
                                    {
                                        if (numFilter[i] == Convert.ToInt32(numbers))
                                        {
                                            if (number_holder == 0)
                                            {
                                                goto END;
                                            }
                                            number_holder = Convert.ToInt32(numbers);
                                            goto END;
                                        }
                                    }
                                }
                                switch (numbers)
                                {
                                    case "1":
                                    case "2":
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                    case "8":
                                    case "9":
                                    case "640":
                                    case "720":
                                    case "1080":
                                    case "1920":
                                    case "2160":
                                    case "2010":
                                        if (number_holder == 0)
                                        {
                                            goto END;
                                        }
                                        number_holder = Convert.ToInt32(numbers);
                                        goto END;
                                }
                                converted = Convert.ToInt32(numbers);
                            END:;
                                numbers = null;
                            }
                            numbers_together = 0;
                            break;
                    }
                }
                if (converted + number_holder == number_holder)
                    return number_holder;
                //converted + num = num that means that the season or resolution filter worked but was not necessery
                return converted;
            }

        }

    }
}
