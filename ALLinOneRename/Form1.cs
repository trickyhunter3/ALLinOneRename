using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace ALLinOneRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly Color alertColor = Color.DarkRed;
        readonly Color approveColor = Color.DarkGreen;

        private void BtnRenameVOne_Click(object sender, EventArgs e)
        {
            string[] lines = FormatTextIntoLines(TbxPath.Text);
            for (int ind = 0; ind < lines.Length; ind++)
            {
                if (Directory.Exists(lines[ind]))
                {
                    int[] numFilter;

                    string usersPath = lines[ind];
                    if (usersPath[usersPath.Length - 1] != '\\')
                        usersPath += '\\';

                    bool isNumberFirst = CbxIsNumberFirstV1.Checked;
                    bool isNumberLast = CbxIsNumberLast.Checked;

                    if (TbxFilterNumbersV1.Text != "")
                    {
                        string[] stringToNum;

                        stringToNum = TbxFilterNumbersV1.Text.Split(' ');
                        numFilter = new int[stringToNum.Length];

                        for (int i = 0; i < stringToNum.Length; i++)
                        {
                            if (stringToNum[i] == "")
                                continue;
                            numFilter[i] = Convert.ToInt32(stringToNum[i]);
                        }
                    }
                    else
                        numFilter = null;

                    DirectoryInfo directoryInfo = new DirectoryInfo(usersPath);
                    FileInfo[] infos = directoryInfo.GetFiles();

                    bool renameSuccess = true;

                    Dictionary<string, int> dict = CreateDictionary(usersPath);//for V2 GetNumber...

                    foreach (FileInfo fileInfo in infos)
                    {
                        try
                        {
                            if (IsFileFilter(fileInfo.Name))
                                goto END;   //filter file names

                            int numberFromTheString;


                            if (CbxIsV2Func.Checked)
                            {
                                numberFromTheString = GetNumberOutOfStringV2(dict, Path.GetFileNameWithoutExtension(fileInfo.Name));
                            }
                            else
                            {
                                numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileInfo.Extension, isNumberFirst, isNumberLast, numFilter);
                            }
                            File.Move(fileInfo.FullName, usersPath + numberFromTheString + fileInfo.Extension);

                            SetCursorDown();
                            AppendColoredTextToRtb(RtbRenamedText, fileInfo.Name + "--> " + numberFromTheString.ToString() + Environment.NewLine, Color.Blue);
                        }
                        catch (IOException)
                        {
                            renameSuccess = false;
                            AppendColoredTextToRtb(RtbRenamedText, fileInfo.Name + " Already exist\n", alertColor);
                        }
                        END:;
                    }
                    if (renameSuccess)
                    {
                        SetCursorDown();
                        AppendColoredTextToRtb(RtbRenamedText, "Done Successfully\n", approveColor);
                    }
                }
                else
                {
                    SetCursorDown();
                    AppendColoredTextToRtb(RtbRenamedText, "PATH DOES NOT EXIST\n", alertColor);
                }
            }
            if (lines.Length < 1)
            {
                SetCursorDown();
                AppendColoredTextToRtb(RtbRenamedText, "PATH DOES NOT EXIST\n", alertColor);
            }
        }

        private void BtnRenameVTwo_Click(object sender, EventArgs e)    
        {
            string[] lines = FormatTextIntoLines(TbxPath.Text);
            for (int ind = 0; ind < lines.Length; ind++)
            {
                if (Directory.Exists(lines[ind]))
                {
                    int[] numFilter;

                    string usersPath = lines[ind];
                    if (usersPath[usersPath.Length - 1] != '\\')
                        usersPath += '\\';

                    bool isNumberFirst = CbxIsNumberFirstV1.Checked;
                    bool isNumberLast = CbxIsNumberLast.Checked;
                    if (TbxFilterNumbersV1.Text != "")
                    {
                        string[] stringToNum;

                        stringToNum = TbxFilterNumbersV1.Text.Split(' ');
                        numFilter = new int[stringToNum.Length];

                        for (int i = 0; i < stringToNum.Length; i++)
                        {
                            if (stringToNum[i] == "")
                                continue;
                            numFilter[i] = Convert.ToInt32(stringToNum[i]);
                        }
                    }
                    else
                        numFilter = null;

                    //get the directory info files and check if there is a path
                    DirectoryInfo directoryInfo = new DirectoryInfo(usersPath);
                    FileInfo[] infos = directoryInfo.GetFiles();

                    bool renameSuccess = true;

                    Dictionary<string, int> dict = CreateDictionary(usersPath);//for V2 GetNumber...

                    foreach (FileInfo fileInfo in infos)
                    {

                        if (!IsFileLocked(fileInfo))
                        {
                            try
                            {
                                if (IsFileFilter(fileInfo.Name))
                                    goto END;       //filter file names

                                int numberFromTheString = 0;

                                if (CbxIsV2Func.Checked)
                                {
                                    numberFromTheString = GetNumberOutOfStringV2(dict, Path.GetFileNameWithoutExtension(fileInfo.Name));//must create dictionary first
                                }
                                else
                                {
                                    numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileInfo.Extension, isNumberFirst, isNumberLast, numFilter);
                                }
                                string seasonNum;                                  //Season number of the current file

                                string[] seasonAndNumberSplited = fileInfo.Directory.Name.Split(' ');          //if the directory has season in it, will fail if no space

                                if (seasonAndNumberSplited[0].ToLower() == "season")                           //if the directory name is season 
                                {
                                    string seriesName = fileInfo.Directory.Parent.Name;

                                    seasonNum = seasonAndNumberSplited[1];

                                    string finalName = CreateFinalName(numberFromTheString, seasonNum, seriesName);

                                    File.Move(fileInfo.FullName, usersPath + finalName + fileInfo.Extension);

                                    SetCursorDown();
                                    AppendColoredTextToRtb(RtbRenamedText, fileInfo.Name + " --> " + finalName + Environment.NewLine, Color.Blue);
                                }

                                else //if directory name was not season then create a season folder
                                {
                                    string seriesName = fileInfo.Directory.Name;

                                    seasonNum = "1"; //set SeasonNum to 1

                                    string finalName = CreateFinalName(numberFromTheString, seasonNum, seriesName);

                                    string newPath = fileInfo.DirectoryName + '\\' + "Season " + seasonNum + '\\';

                                    if (!Directory.Exists(newPath))
                                    {
                                        RtbRenamedText.Focus();

                                        RtbRenamedText.AppendText("Created Folder: Season " + seasonNum + Environment.NewLine);
                                        Directory.CreateDirectory(newPath);
                                    }

                                    File.Move(fileInfo.FullName, newPath + finalName + fileInfo.Extension);

                                    SetCursorDown();
                                    AppendColoredTextToRtb(RtbRenamedText, fileInfo.Name + " --> " + finalName + Environment.NewLine, Color.Blue);
                                }
                            }
                            catch (IOException)
                            {
                                renameSuccess = false;
                                AppendColoredTextToRtb(RtbRenamedText, fileInfo.Name + " Already exist\n", alertColor);
                            }
                            END:;
                        }
                        else
                        {
                            renameSuccess = false;
                            AppendColoredTextToRtb(RtbRenamedText, fileInfo.Name + "Is being used\n", alertColor);
                        }
                    }

                    if (renameSuccess)
                    {
                        SetCursorDown();
                        AppendColoredTextToRtb(RtbRenamedText, "Done Successfully\n", approveColor);
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
                        else if (Convert.ToInt32(seasonNum) <= 9)
                            return "S0" + seasonNum + episodeHelper + numberFromTheString.ToString();
                        else
                            return "S" + seasonNum + episodeHelper + numberFromTheString.ToString();
                    }
                }
                else
                {
                    SetCursorDown();
                    AppendColoredTextToRtb(RtbRenamedText, "PATH DOES NOT EXIST\n", alertColor);
                }
            }
            if(lines.Length < 1)
            {
                SetCursorDown();
                AppendColoredTextToRtb(RtbRenamedText, "PATH DOES NOT EXIST\n", alertColor);
            }
        }

        private void BtnSubtractName_Click(object sender, EventArgs e)//only works if the numbers are -  1,2,3,4...
        {
            if (Directory.Exists(TbxPath.Text))
            {
                var isNumeric = int.TryParse(TbxSubtractNumber.Text, out _);
                if (TbxSubtractNumber.Text != "" && isNumeric)
                {
                    string usersPath = TbxPath.Text;
                    if (usersPath[usersPath.Length - 1] != '\\')
                        usersPath += '\\';

                    //get the directory info files and check if there is a path
                    DirectoryInfo directoryInfo = new DirectoryInfo(usersPath);
                    FileInfo[] infos = directoryInfo.GetFiles();

                    int subtractAmount = Convert.ToInt32(TbxSubtractNumber.Text);
                    int subtracted;

                    string converted;

                    if (infos.Length < Math.Abs(subtractAmount))
                    {
                        foreach (FileInfo fileInfo in infos)
                        {
                            if (fileInfo.Name != "desktop.ini")
                            {
                                if (fileInfo.Name != "icon.ico")
                                {
                                    string originalFileName = fileInfo.Name;

                                    converted = Path.GetFileNameWithoutExtension(fileInfo.FullName);

                                    subtracted = Convert.ToInt32(converted) - subtractAmount;

                                    File.Move(fileInfo.FullName, usersPath + subtracted.ToString() + fileInfo.Extension);

                                    SetCursorDown();
                                    AppendColoredTextToRtb(RtbRenamedText, originalFileName + " --> " + subtracted.ToString() + Environment.NewLine, Color.Blue);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (FileInfo fileInfo in infos)
                        {
                            converted = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                            if (converted != "desktop.ini")
                            {
                                if (fileInfo.Name != "icon.ico")
                                {
                                    subtracted = Convert.ToInt32(converted) + infos.Length; //add 

                                    File.Move(fileInfo.FullName, usersPath + subtracted.ToString() + fileInfo.Extension);
                                }
                            }
                        }

                        infos = directoryInfo.GetFiles();//get the new files

                        foreach (FileInfo fileInfo in infos)
                        {
                            converted = Path.GetFileNameWithoutExtension(fileInfo.FullName);

                            if (converted != "desktop.ini")
                            {
                                if (fileInfo.Name != "icon.ico")
                                {
                                    string originalFileName = (Convert.ToInt32(converted) - infos.Length).ToString();

                                    subtracted = Convert.ToInt32(converted) - infos.Length - subtractAmount;//then subtract all 

                                    File.Move(fileInfo.FullName, usersPath + subtracted.ToString() + fileInfo.Extension);

                                    SetCursorDown();
                                    AppendColoredTextToRtb(RtbRenamedText, originalFileName + " --> " + subtracted.ToString() + Environment.NewLine, Color.Blue);
                                }
                            }
                        }
                    }
                }
                else
                {
                    SetCursorDown();
                    AppendColoredTextToRtb(RtbRenamedText, "No \"Subtract\" number detected\n", alertColor);
                }
            }
            else
            {
                SetCursorDown();
                AppendColoredTextToRtb(RtbRenamedText, "PATH DOES NOT EXIST\n", alertColor);
            }
        }

        private void BtnCheckFiles_Click(object sender, EventArgs e)
        {
            RtbCheckFiles.Clear();
            //will check 2 directories and their subdirectories

            string InvalidSeasonName = null;
            bool WasThereAnyInvalid = false;

            //get paths from xml file
            string firstPath = "", secondPath = "";
            var reader = XmlReader.Create("Paths.xml");
            reader.ReadToFollowing("path");
            do
            {
                reader.MoveToFirstAttribute();
                if (reader.Value == "Anime")
                {
                    reader.ReadToFollowing("value");
                    firstPath = reader.ReadElementContentAsString();
                }
                if (reader.Value == "Anime Not")
                {
                    reader.ReadToFollowing("value");
                    secondPath = reader.ReadElementContentAsString();
                    break;
                }
            } while (reader.ReadToFollowing("path"));

            reader.Close();
            reader.Dispose();

            string root = firstPath;

            string[] subdirectoryEntriesEntry = Directory.GetDirectories(root);
            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntriesEntry)
                LoadSubDirs(subdirectory);
            
            // Get all subdirectories
            root = secondPath;
            subdirectoryEntriesEntry = Directory.GetDirectories(root);

            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntriesEntry)
                LoadSubDirs(subdirectory);
            
            if (WasThereAnyInvalid)
                AppendColoredTextToRtb(RtbCheckFiles, "\nProblem\n" + InvalidSeasonName + Environment.NewLine, alertColor);
            else
                AppendColoredTextToRtb(RtbCheckFiles, "\nEverything is ok\n", approveColor);

            void LoadSubDirs(string dir)
            {
                RtbCheckFiles.AppendText(dir + Environment.NewLine);//display current location

                bool IsfirstTime = true;
                string filterPath = dir + '\\' + "Plex Versions";

                string[] subdirectoryEntries = Directory.GetDirectories(dir);
                /*
                        only check the episodes if it's the last folder inside a folder 
                        and you can change to not enter the folder named "Plex Versions" 
                */

                if (subdirectoryEntries.Length < 1) // add "|| filterPath == subdirectoryEntries[0]" to if to filter folder
                {
                    bool thisTrySucess = true;

                    DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                    FileInfo[] infos = directoryInfo.GetFiles();

                    foreach (FileInfo fileInfo in infos)
                    {
                        string seriesName = fileInfo.Directory.Parent.Name + " - ";
                        string inFileSeriesName = fileInfo.Name.Split(' ')[0] + " - ";

                        string[] seasonArray = fileInfo.Directory.Name.Split(' ');
                        string season = "random";//this will change if needed in the next if
                        if (seasonArray.Length > 1)
                            season = seasonArray[1];

                        if (!IsVaild(Path.GetFileNameWithoutExtension(fileInfo.FullName), fileInfo.Name, fileInfo.Extension, season, seriesName))
                        {
                            AppendColoredTextToRtb(RtbCheckFiles, "Found a problem at:" + fileInfo.DirectoryName + "problem name: " + fileInfo.Name + Environment.NewLine, alertColor);
                            WasThereAnyInvalid = true;
                            if (IsfirstTime)
                            {
                                IsfirstTime = false;
                                InvalidSeasonName += fileInfo.DirectoryName + Environment.NewLine + Environment.NewLine;
                            }
                        }
                    }
                    if (thisTrySucess) 
                        AppendColoredTextToRtb(RtbCheckFiles, "Passed\n", approveColor);
                }
                else
                {
                    // don't enter the "Plex Versions" folder
                    //if (filterPath != subdirectoryEntries[0]) <-- uncomment this to not enter "Plex Versions" folder
                    foreach (string subdirectory in subdirectoryEntries)
                        LoadSubDirs(subdirectory);
                }
            }

            bool IsVaild(string NameNoType, string name, string type, string Season, string SeriesName)
            {
                if (IsFileFilter(name))
                    return true;

                //int num = GetNumberOutOfStringV2(dict, NameNoType); //10 ms longer and won't work 
                int num = GetNumberOutOfString(name, type);

                //check if Season is a number
                bool bNum = int.TryParse(Season, out int i);
                if (!bNum)
                    return false;

                //check if name format is correct like this --> SeriesName - S SeasonNumber E EpisodeNumber ---> example: SeriesName - S01E01
                if (Convert.ToInt32(Season) / 10 == 0)
                {
                    //episode is less then 10 -> there is a need for "0 helper"
                    if (num / 10 == 0)
                        return IsFormatCorrect(NameNoType, SeriesName, Season, num, "E0");

                    //episode is more then 10 -> there is no need for "0 helper"
                    else
                        return IsFormatCorrect(NameNoType, SeriesName, Season, num, "E");
                }
                else
                {
                    //episode is less then 10 -> there is a need for "0 helper"
                    //"0 helper" is the 0 before the episode or the season if needed
                    if (num / 10 == 0)
                    {
                        if (NameNoType == SeriesName + "S" + Season + "E0" + num)
                            return true;
                        if (NameNoType == SeriesName + "S" + Season + "E0" + (num - 1) + "-" + "E0" + num)//more then 1 episode in a video 
                            return true;
                        if (NameNoType == SeriesName + "S" + Season + "E0" + num + " - part1")
                            return true;
                        if (NameNoType == SeriesName + "S" + Season + "E0" + num + " - part2")
                            return true;
                    }
                    //episode is more then 10 -> there is no need for "0 helper"
                    else
                    {
                        if (NameNoType == SeriesName + "S" + Season + "E" + num)
                            return true;
                        if (NameNoType == SeriesName + "S" + Season + "E" + (num - 1) + "-" + "E" + num)//more then 1 episode in a video 
                            return true;
                        if (NameNoType == SeriesName + "S" + Season + "E" + num + " - part1")
                            return true;
                        if (NameNoType == SeriesName + "S" + Season + "E" + num + " - part2")
                            return true;
                    }
                }
                return false;
            }

            bool IsFormatCorrect(string NameNoType, string SeriesName, String Season, int num, string EpisodeHolder)
            {
                if (Season == "00")
                {
                    if (NameNoType == SeriesName + "S" + Season + EpisodeHolder + num)
                        return true;
                    if(NameNoType == SeriesName + "S" + Season + EpisodeHolder + num + "-" + EpisodeHolder + (num + 1))//more then 1 episode in a video 
                        return true;
                }
                if (NameNoType == SeriesName + "S0" + Season + EpisodeHolder + num)
                    return true;
                if (NameNoType == SeriesName + "S0" + Season + EpisodeHolder + (num-1) + "-" + EpisodeHolder + num)//more then 1 episode in a video 
                    return true;

                    return false;
            }
        }

        private void BtnShowFolderFiles_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TbxPath.Text))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(TbxPath.Text);
                FileInfo[] infos = directoryInfo.GetFiles();

                foreach(FileInfo fileInfo in infos)
                {
                    SetCursorDown();
                    if (IsFileFilter(fileInfo.Name))//custom icons to folders
                        RtbRenamedText.AppendText(fileInfo.Name + " - Ignored\n");
                    else
                        RtbRenamedText.AppendText(fileInfo.Name + "\n");
                }
            }
            else
            {
                SetCursorDown();
                AppendColoredTextToRtb(RtbRenamedText, "PATH DOES NOT EXIST\n", alertColor);
            }
        }

        private void BtnOpenExplorer_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TbxPath.Text))
            {
                System.Diagnostics.Process.Start("explorer.exe", TbxPath.Text);
            }
            else
            {
                SetCursorDown();
                AppendColoredTextToRtb(RtbRenamedText, "PATH DOES NOT EXIST\n", alertColor);
            }
        }

        private int GetNumberOutOfString(string File_name, string file_type, bool isFirst = false, bool isLast = false, int[] numFilter = null)
        {
            // j is current index of the file_name 
            int converted = 0;
            string numLast = "";//remember every number to know the last one
            //if we find a number that is episode then i++ happen so we save the episode number and 
            //on the next run when it find a season number or resolution number it will go to 0 on the next int not on the
            //episode number itself
            int numbers_together = 0;
            //when he find number he start to count so that it won't check if statement IF he is not at least 1 number
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
                        numbers += File_name[j];//start recording the numbers if they are found
                        break;
                    default:
                        if (numbers_together != 0)
                        {
                            if (isFirst)//if number is first then return the number 
                                return Convert.ToInt32(numbers);
                            if (isLast)
                                numLast = numbers;

                            if (numbers + file_type == File_name)
                            {
                                converted = Convert.ToInt32(numbers);
                                number_holder = 0;
                                goto END;           //if file is just a number then returns that number
                            }

                            if (numbers == "0")
                                number_holder = 0;

                            if (numFilter != null)
                            {
                                for (int i = 0; i < numFilter.Length; i++)
                                {
                                    if (numFilter[i] == Convert.ToInt32(numbers))
                                    {
                                        if (number_holder != 0)
                                        {
                                            number_holder = Convert.ToInt32(numbers);
                                        }
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
                                case "480":
                                case "640":
                                case "720":
                                case "1080":
                                case "1280":
                                case "1920":
                                case "2010":
                                case "2160":
                                case "3840":
                                    if (number_holder != 0)
                                    {
                                        number_holder = Convert.ToInt32(numbers);
                                    }
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
            if (isLast)
                return Convert.ToInt32(numLast);
            if (converted + number_holder == number_holder)
                return number_holder;
            //converted + num = num that means that the season or resolution filter worked but was not necessary

            return converted;
        }

        private void BtnTransferFilesFromDownload_Click(object sender, EventArgs e) // work in progress
        {
            /*
                works when " FileName - someting.Extension " 
                takes the string "Filename - something" and split by("-")
                then searches the last season in the folder if exist
                then places that episode there
            */
            //get Paths from xml file
            string firstPath = "", secondPath = "", downloadPath = "";
            var reader = XmlReader.Create("Paths.xml");
            reader.ReadToFollowing("path");
            do
            {
                reader.MoveToFirstAttribute();
                if (reader.Value == "Anime")
                {
                    reader.ReadToFollowing("value");
                    firstPath = reader.ReadElementContentAsString();
                }
                if (reader.Value == "Anime Not")
                {
                    reader.ReadToFollowing("value");
                    secondPath = reader.ReadElementContentAsString();
                }
                if (reader.Value == "Download")
                {
                    reader.ReadToFollowing("value");
                    downloadPath = reader.ReadElementContentAsString();
                }
            } while (reader.ReadToFollowing("path"));

            reader.Close();
            reader.Dispose();

            //if (File.Exists("Dictionary"))
            
            string[] subdirectoryEntriesEntry = Directory.GetDirectories(downloadPath);

            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntriesEntry)
                LoadSubDirs(subdirectory);
            
            //else
           
            //    AppendColoredTextToRtb(RtbRenamedText, "Please use the Create button\n", Color.DarkRed);
            
            
            void LoadSubDirs(string dir)
            {
                RtbRenamedText.AppendText(dir + Environment.NewLine);//plain black text faster to write

                string[] subdirectoryEntries = Directory.GetDirectories(dir);

                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                FileInfo[] infos = directoryInfo.GetFiles();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (FileInfo fileInfo in infos)
                {
                    string pathNeeded;
                    string SeasonNum;
                    string SeriesName = GetSeriesName(fileInfo.Directory.Name);

                    if (Directory.Exists(firstPath + SeriesName + '\\'))
                    {
                        SeasonNum = GetLatestSeason(SeriesName, firstPath);
                        pathNeeded = SeriesName + '\\' + "Season " + SeasonNum + '\\';
                        File.Move(fileInfo.FullName, firstPath + pathNeeded + fileInfo.Name);
                    }
                    else if (Directory.Exists(secondPath + SeriesName + '\\'))
                    {
                        SeasonNum = GetLatestSeason(SeriesName, secondPath);
                        pathNeeded = SeriesName + '\\' + "Season " + SeasonNum + '\\';
                        File.Move(fileInfo.FullName, secondPath + pathNeeded + fileInfo.Name);
                    }
                    else
                    {
                        AppendColoredTextToRtb(RtbRenamedText, "Directory not found", Color.DarkRed);

                    }
                    AppendColoredTextToRtb(RtbRenamedText, "Moved file: " + fileInfo.Name, Color.Blue);
                }
                Directory.Delete(dir);
                foreach (string subdirectory in subdirectoryEntries)
                    LoadSubDirs(subdirectory);
            }

            string GetLatestSeason(string SeriesName, string dest)
            {
                string[] currentSubdirectory = Directory.GetDirectories(dest);
                //find the series then the max season
                foreach (string directory in currentSubdirectory)
                {
                    if(dest + SeriesName == directory)
                    {
                        int j = dest.Length + SeriesName.Length + 1 + 6;
                        string[] seasons = Directory.GetDirectories(directory);
                        int[] seasonsInNum = new int[seasons.Length];
                        for(int i = 0; i < seasons.Length; i++)
                        {
                            seasonsInNum[i] = Convert.ToInt32(seasons[i].Substring(dest.Length + SeriesName.Length + 7));//7 is the slash and the season
                        }
                        int maxValue = seasonsInNum.Max();
                        return maxValue.ToString();
                    }
                }
                return "Error";///did not find the series in folder
            }

            string GetSeriesName(string directoryName)
            {
                string[] nameSplited = directoryName.Split('-');
                string result = "";
                for(int i = 0; i < nameSplited.Length - 1; i++)
                {
                    result = result + nameSplited[i] + "-";
                }
                result = result.Substring(0, result.Length - 2);
                //string result = directoryName.Split('-')[0];
                //result = result.Substring(0, result.Length - 1); // no need the space at the end

                return result;
            }

        }

        private string[] FormatTextIntoLines(string text)
        {
            return text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void AppendColoredTextToRtb(RichTextBox rtb, string text, Color _color)
        {
            rtb.Focus();
            rtb.SelectionColor = _color;
            rtb.SelectedText += text;
        }

        private void SetCursorDown()
        {
            RtbRenamedText.Select(RtbRenamedText.Text.Length, 0);
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

        private void BtnClearRenamedRtb_Click(object sender, EventArgs e)
        {
            RtbRenamedText.Clear();
        }

        private void BtnClearCheckFilesRtb_Click(object sender, EventArgs e)
        {
            RtbCheckFiles.Clear();
        }

        private bool IsFileFilter(string FileName)
        {
            switch (FileName)
            {
                case "desktop.ini":
                case "icon.ico":
                    return true;

                default:
                    break;
            }
            string[] ext = FileName.Split('.');
            if (ext[ext.Length - 1] == "nfo")
                return true;
            return false;
        }

        private void BtnCreateHashTable_Click(object sender, EventArgs e)
        {

            //get Paths from Xml file
            string firstPath = "", secondPath = "";
            var reader = XmlReader.Create("Paths.xml");
            reader.ReadToFollowing("path");
            do
            {
                reader.MoveToFirstAttribute();
                if (reader.Value == "Anime")
                {
                    reader.ReadToFollowing("value");
                    firstPath = reader.ReadElementContentAsString();
                }
                if (reader.Value == "Anime Not")
                {
                    reader.ReadToFollowing("value");
                    secondPath = reader.ReadElementContentAsString();
                    break;
                }
            } while (reader.ReadToFollowing("path"));
            reader.Close();
            reader.Dispose();
            //this function is not needed, 3 hours of waste
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string root = firstPath;
                using (StreamWriter writer = new StreamWriter("Dictionary.txt"))
                {
                    string[] subdirectoryEntries = Directory.GetDirectories(root);

                    foreach (string subdirectory in subdirectoryEntries)
                    {
                        string hash = GetHash(sha256Hash, subdirectory);
                        hash = hash.Substring(0, 15);
                        writer.Write(hash + '|' + subdirectory + '|');
                    }
                    root = secondPath;
                    subdirectoryEntries = Directory.GetDirectories(root);

                    foreach (string subdirectory in subdirectoryEntries)
                    {
                        string hash = GetHash(sha256Hash, subdirectory);
                        hash = hash.Substring(0, 15);
                        writer.Write(hash + '|' + subdirectory + '|');
                    }
                }
            }

            string GetHash(HashAlgorithm hashAlgorithm, string input)
            {

                // Convert the input string to a byte array and compute the hash.
                byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        private int GetNumberOutOfStringV2(Dictionary<string, int> dict, string filename)
        {
            /*
                must have at least 2 files
                machine learning ? idk

                how works:
                takes all the files in the folder
                gets all the numbers in the file names
                gets all the numbers in the file names and count them
                use the the most NOT used number
                that probably the number i need
                
                exeptions and solutions:
                
                1.
                if there is more then one most NOT used number
                but there is a most used number that is 1 above every other number
                (example: [AniFilm] Tonari no Kaibutsu-kun [TV] [13 of 13] [HDTVRip 1280x720 x264] [Ru Jp] [DemonOFmooN & MezIdA],
                all the other files are the same stracture just the episode is changing - "1 of 13" and so on)
                solution will be: 
                to check if there is more then 1 most NOT used number
                and if true then use the most used number
            */
            string[] nums = Regex.Split(filename, @"\D+");
            nums = nums.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (int i = 0; i < nums.Length; i++)
            {

                dict.TryGetValue(nums[i], out int val);
                if (val == 1)
                    return Convert.ToInt32(nums[i]);
            }
            dict.TryGetValue(nums[0], out int max);
            int current;
            int index = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                dict.TryGetValue(nums[i], out current);
                if (max < current)
                {
                    max = current;
                    index = i;
                }
            }
                return Convert.ToInt32(nums[index]);
        }

        private Dictionary<string, int> CreateDictionary(string path)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] infos = directoryInfo.GetFiles();

            foreach (FileInfo fileInfo in infos)
            {
                if (IsFileFilter(fileInfo.Name)) continue;
                ///get all the numbers from a string
                string[] numbers = Regex.Split(fileInfo.Name, @"\D+");
                int val;
                for(int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == "") continue;
                    if (dict.ContainsKey(numbers[i]))
                    {
                        //appending the value of the dict in that place
                        dict.TryGetValue(numbers[i], out val);
                        val++;
                        dict.Remove(numbers[i]);
                        dict.Add(numbers[i], val);
                    }
                    else
                    {
                        dict.Add(numbers[i], 1);//found at least 1
                    }
                }
            }

            return dict;
        }

    }
}
