using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;

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

                    bool isNumberFirst = CbxIsNumberFirstV2.Checked;

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

                    foreach (FileInfo fileInfo in infos)
                    {
                        try
                        {
                            if (IsFileFilter(fileInfo.Name))
                                goto END;   //filter file names

                            int numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileInfo.Extension, isNumberFirst, numFilter);

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

                    bool isNumberFirst = CbxIsNumberFirstV2.Checked;
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

                    foreach (FileInfo fileInfo in infos)
                    {

                        if (!IsFileLocked(fileInfo))
                        {
                            try
                            {
                                if (IsFileFilter(fileInfo.Name))
                                    goto END;               //filter file names

                                int numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileInfo.Extension, isNumberFirst, numFilter);

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

            string root = @"D:\AN\Anime";

            string[] subdirectoryEntriesEntry = Directory.GetDirectories(root);

            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntriesEntry)
                LoadSubDirs(subdirectory);

            // Get all subdirectories
            root = @"D:\AN\Anime not";
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

                int num = GetNumberOutOfString(name, type, false);

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

        private int GetNumberOutOfString(string File_name, string file_type, bool isFirst, int[] numFilter = null)
        {
            // j is current index of the file_name 
            int converted = 0;
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

            const string FILES_PATH = @"D:\torrent download\";
            const string DESTINATION_PATH = @"D:\AN\Anime\";

            string[] subdirectoryEntriesEntry = Directory.GetDirectories(FILES_PATH);

            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntriesEntry)
                LoadSubDirs(subdirectory);
            
            void LoadSubDirs(string dir)
            {
                RtbRenamedText.AppendText(dir + Environment.NewLine);

                string[] subdirectoryEntries = Directory.GetDirectories(dir);

                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                FileInfo[] infos = directoryInfo.GetFiles();

                foreach (FileInfo fileInfo in infos)
                {
                    string SeriesName = GetSeriesName(fileInfo.Directory.Name);
                    string SeasonNum = GetLatestSeason(SeriesName);
                    string pathNeeded = SeriesName + '\\' + "Season " + SeasonNum + '\\';
                    File.Move(fileInfo.FullName, DESTINATION_PATH + pathNeeded + fileInfo.Name);
                    AppendColoredTextToRtb(RtbRenamedText, "Moved file: " + fileInfo.Name, Color.Blue);
                }
                Directory.Delete(dir);
                foreach (string subdirectory in subdirectoryEntries)
                    LoadSubDirs(subdirectory);
            }

            string GetLatestSeason(string SeriesName)
            {
                string[] currentSubdirectory = Directory.GetDirectories(DESTINATION_PATH);
                //find the series then the max season
                foreach (string directory in currentSubdirectory)
                {
                    if(DESTINATION_PATH + SeriesName == directory)
                    {
                        int j = DESTINATION_PATH.Length + SeriesName.Length + 1 + 6;
                        string[] seasons = Directory.GetDirectories(directory);
                        int[] seasonsInNum = new int[seasons.Length];
                        for(int i = 0; i < seasons.Length; i++)
                        {
                            seasonsInNum[i] = Convert.ToInt32(seasons[i].Substring(DESTINATION_PATH.Length + SeriesName.Length + 7));//7 is the slash and the season
                        }
                        int maxValue = seasonsInNum.Max();
                        return maxValue.ToString();
                    }
                }
                return "Error";///did not find the series in folder
            }

            string GetSeriesName(string directoryName)
            {
                string result = directoryName.Split('-')[0];
                result = result.Substring(0, result.Length - 1); // no need the space at the end

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
                    return false;
            }
        }
    }
}
