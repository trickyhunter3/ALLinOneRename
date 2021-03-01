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
            if (Directory.Exists(TbxPath.Text))
            {
                int[] numFilter;
                bool needFilter = false;

                string usersPath = TbxPath.Text;
                if (usersPath[usersPath.Length - 1] != '\\')
                    usersPath += '\\';

                if (TbxFilterNumbersV1.Text != "")
                {
                    string[] stringToNum;

                    needFilter = true;
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
                {
                    numFilter = new int[0];//compile error if not this
                }

                bool isNumberFirst = CbxIsNumberFirstV1.Checked;
                string numberSide;
                if (isNumberFirst)
                {
                    numberSide = "f";//number is first
                }
                else
                {
                    numberSide = ""; //not first
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(usersPath);
                FileInfo[] infos = directoryInfo.GetFiles();

                bool renameSuccess = true;

                foreach (FileInfo fileInfo in infos)
                {
                    try
                    {
                        if (fileInfo.Name == "desktop.ini" || fileInfo.Name == "icon.ico")
                            goto END;   //filter file names

                        int numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileInfo.Extension, numberSide);

                        File.Move(fileInfo.FullName, usersPath + numberFromTheString + fileInfo.Extension);

                        SetCursorDown(RtbRenamedText);

                        RtbRenamedText.SelectionColor = Color.Blue;
                        RtbRenamedText.SelectedText += fileInfo.Name + "--> " + numberFromTheString.ToString() + Environment.NewLine;
                        RtbRenamedText.ScrollToCaret();
                    }
                    catch (IOException)
                    {
                        renameSuccess = false;

                        SetCursorDown(RtbRenamedText);

                        RtbRenamedText.SelectionColor = Color.Red;
                        RtbRenamedText.SelectedText = fileInfo.Name + " Already exist" + Environment.NewLine;
                        RtbRenamedText.ScrollToCaret();
                    }
                END:;
                }
                if (renameSuccess)
                {
                    SetCursorDown(RtbRenamedText);

                    RtbRenamedText.SelectionColor = Color.Green;
                    RtbRenamedText.SelectedText = "Done Successfully" + Environment.NewLine;
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
                                numbers += File_name[j];//start recording the numbers if they are found
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
                                        goto END;           //if file is just a number then returns that number
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
            else
            {
                SetCursorDown(RtbRenamedText);

                RtbRenamedText.SelectionColor = Color.Red;
                RtbRenamedText.SelectedText += "PATH DOES NOT EXIST" + Environment.NewLine;
                RtbRenamedText.ScrollToCaret();
            }
        }

        private void BtnRenameVTwo_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TbxPath.Text))
            {
                int[] numFilter;
                bool needFilter = false;

                string usersPath = TbxPath.Text;
                if (usersPath[usersPath.Length - 1] != '\\')
                    usersPath += '\\';

                if (TbxFilterNumbersV2.Text != "")
                {
                    string[] stringToNum;

                    needFilter = true;
                    stringToNum = TbxFilterNumbersV2.Text.Split(' ');
                    numFilter = new int[stringToNum.Length];
                    for (int i = 0; i < stringToNum.Length; i++)
                    {
                        if (stringToNum[i] == "")
                            continue;
                        numFilter[i] = Convert.ToInt32(stringToNum[i]);
                    }

                }
                else
                {
                    numFilter = new int[0];//compile error if not this
                }

                bool isNumberFirst = CbxIsNumberFirstV2.Checked;
                string numberSide;

                if (isNumberFirst)
                    numberSide = "f";//number is first
                else
                    numberSide = null; //not first

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
                            if (fileInfo.Name == "desktop.ini" || fileInfo.Name == "icon.ico")
                                goto END;               //filter file names

                            string seasonNum;                                  //Season number of the current file

                            string[] seasonAndNumberSplited = fileInfo.Directory.Name.Split(' ');          //if the directory has season in it, will fail if no space

                            if (seasonAndNumberSplited[0].ToLower() == "season")                           //if the directory name is season 
                            {
                                string seriesName = fileInfo.Directory.Parent.Name;//the Series name of the current file
                                int numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileInfo.Extension, numberSide);
                                seasonNum = seasonAndNumberSplited[1];

                                string finalName = CreateFinalName(numberFromTheString, seasonNum, seriesName);

                                File.Move(fileInfo.FullName, usersPath + finalName + fileInfo.Extension);

                                SetCursorDown(RtbRenamedText);

                                RtbRenamedText.SelectionColor = Color.Blue;
                                RtbRenamedText.SelectedText += fileInfo.Name + " --> " + finalName + Environment.NewLine;
                                RtbRenamedText.ScrollToCaret();
                            }

                            else //if directory name was not season
                            {
                                string seriesName = fileInfo.Directory.Name;//the Series name of the current file
                                int numberFromTheString = GetNumberOutOfString(fileInfo.Name, fileInfo.Extension, numberSide);

                                seasonNum = "1"; //set SeasonNum to 1

                                string finalName = CreateFinalName(numberFromTheString, seasonNum, seriesName);

                                string newPath = fileInfo.DirectoryName + '\\' + "Season " + seasonNum + '\\';

                                if (!Directory.Exists(newPath))
                                {
                                    SetCursorDown(RtbRenamedText);

                                    RtbRenamedText.Text += "Created Folder: Season " + seasonNum + Environment.NewLine;
                                    Directory.CreateDirectory(newPath);
                                }

                                File.Move(fileInfo.FullName, newPath + finalName + fileInfo.Extension);

                                SetCursorDown(RtbRenamedText);

                                RtbRenamedText.SelectionColor = Color.Blue;
                                RtbRenamedText.SelectedText += fileInfo.Name + " --> " + finalName + Environment.NewLine;
                                RtbRenamedText.ScrollToCaret();
                            }
                        }
                        catch (IOException)
                        {
                            renameSuccess = false;

                            SetCursorDown(RtbRenamedText);

                            RtbRenamedText.SelectionColor = Color.Red;
                            RtbRenamedText.SelectedText = fileInfo.Name + " Already exist" + Environment.NewLine;
                            RtbRenamedText.ScrollToCaret();
                        }
                    END:;
                    }
                    else
                    {
                        SetCursorDown(RtbRenamedText);

                        RtbRenamedText.SelectionColor = Color.Red;
                        RtbRenamedText.SelectedText = fileInfo.Name + "Is being used";
                    }
                }
                if (renameSuccess)
                {
                    SetCursorDown(RtbRenamedText);

                    RtbRenamedText.SelectionColor = Color.Green;
                    RtbRenamedText.SelectedText = "Done Successfully" + Environment.NewLine;
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
                                numbers += File_name[j];//start recording the numbers if they are found
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
                                        goto END;           //if file is just a number then returns that number
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
            else
            {
                SetCursorDown(RtbRenamedText);

                RtbRenamedText.SelectionColor = Color.Red;
                RtbRenamedText.SelectedText += "PATH DOES NOT EXIST" + Environment.NewLine;
                RtbRenamedText.ScrollToCaret();
            }
        }

        private void BtnSubtractName_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TbxPath.Text))
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

                if(infos.Length < Math.Abs(subtractAmount))
                {
                    foreach(FileInfo fileInfo in infos)
                    {
                        converted = Path.GetFileNameWithoutExtension(fileInfo.FullName);

                        subtracted = Convert.ToInt32(converted) - subtractAmount;

                        File.Move(fileInfo.FullName, usersPath + subtracted.ToString() + fileInfo.Extension);

                        RtbRenamedText.SelectionColor = Color.Blue;
                        RtbRenamedText.SelectedText += fileInfo.Name + " --> " + subtracted.ToString() + Environment.NewLine;
                        RtbRenamedText.ScrollToCaret();
                    }
                }
                else
                {
                    foreach (FileInfo fileInfo in infos)
                    {
                        converted = Path.GetFileNameWithoutExtension(fileInfo.FullName);

                        subtracted = Convert.ToInt32(converted) + infos.Length; //add 

                        File.Move(fileInfo.FullName, usersPath + subtracted.ToString() + fileInfo.Extension);
                    }

                    infos = directoryInfo.GetFiles();//get the new files

                    foreach (FileInfo fileInfo in infos)
                    {
                        converted = Path.GetFileNameWithoutExtension(fileInfo.FullName);

                        subtracted = Convert.ToInt32(converted) - infos.Length - subtractAmount;//then subtract all 

                        File.Move(fileInfo.FullName, usersPath + subtracted.ToString() + fileInfo.Extension);

                        RtbRenamedText.SelectionColor = Color.Blue;
                        RtbRenamedText.SelectedText += fileInfo.Name + " --> " + subtracted.ToString() + Environment.NewLine;
                        RtbRenamedText.ScrollToCaret();
                    }
                }
            }
            else
            {
                SetCursorDown(RtbRenamedText);

                RtbRenamedText.SelectionColor = Color.Red;
                RtbRenamedText.SelectedText += "PATH DOES NOT EXIST" + Environment.NewLine;
                RtbRenamedText.ScrollToCaret();
            }
        }

        private void BtnCheckFiles_Click(object sender, EventArgs e)
        {
            RtbCheckFiles.Clear();
            //will check 2 directories and their suddirectories

            bool needFilter = false;//must for the GetNumberOutOfString
            int[] numFilter = { 0 };//must for the GetNumberOutOfString

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
            {
                AppendColoredTextToRtb("\n\nProblem\n", Color.Red);
                AppendColoredTextToRtb(InvalidSeasonName, Color.Red);
            }
            else
            {
                AppendColoredTextToRtb("\n\nEverything is ok", Color.Green);
            }

            void LoadSubDirs(string dir)
            {
                AppendColoredTextToRtb(dir, Color.Black);

                bool IsfirstTime = true;

                string[] subdirectoryEntries = Directory.GetDirectories(dir);
                /*
                        only check the episodes if it's the last folder inside a folder 
                        and DOESN'T check if the folder's name is "Plex Versions" 
                */

                if (subdirectoryEntries.Length < 1 || dir + '\\' + "Plex Versions" == subdirectoryEntries[0])
                {
                    bool thisTrySucess = true;

                    DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                    FileInfo[] infos = directoryInfo.GetFiles();

                    foreach (FileInfo fileInfo in infos)
                    {
                        string seriesName = fileInfo.Directory.Parent.Name + " - ";
                        string inFileSeriesName = fileInfo.Name.Split(' ')[0] + " - ";

                        string[] seasonArray = fileInfo.Directory.Name.Split(' ');
                        string season;
                        if (seasonArray.Length > 1)
                            season = seasonArray[1];
                        else
                            season = "dwadaww";//random - should be incorrect

                        if (!IsVaild(Path.GetFileNameWithoutExtension(fileInfo.FullName), fileInfo.Name, fileInfo.Extension, season, seriesName))
                        {
                            AppendColoredTextToRtb("Found a problem at:"
                                                        + fileInfo.DirectoryName
                                                        + "problem name: "
                                                        + fileInfo.Name
                                                        , Color.Red);
                            WasThereAnyInvalid = true;
                            if (IsfirstTime)
                            {
                                IsfirstTime = false;
                                InvalidSeasonName += fileInfo.DirectoryName + Environment.NewLine + Environment.NewLine;
                            }
                        }
                    }
                    if (thisTrySucess) 
                        AppendColoredTextToRtb("Passed", Color.Green);
                }
                if (subdirectoryEntries.Length < 1)
                    goto END;
                // doesn't enter the "Plex Versions" folder
                if (dir + '\\' + "Plex Versions" != subdirectoryEntries[0])
                    foreach (string subdirectory in subdirectoryEntries)
                        LoadSubDirs(subdirectory);
                    END:;
            }

            bool IsVaild(string NameNoType, string name, string type, string Season, string SeriesName)
            {
                if (name == "desktop.ini" || name == "icon.ico")
                    return true;

                int num = GetNumberOutOfString(name, type, "");

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
                    }
                    //episode is more then 10 -> there is no need for "0 helper"
                    else
                    {
                        if (NameNoType == SeriesName + "S" + Season + "E" + num)
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
                }
                if (NameNoType == SeriesName + "S0" + Season + EpisodeHolder + num)
                    return true;


                return false;
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
                            numbers += File_name[j];//start recording the numbers if they are found
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
                                    goto END;           //if file is just a number then returns that number
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

            void AppendColoredTextToRtb(string text, Color _color)
            {
                RtbCheckFiles.SelectionColor = _color;
                RtbCheckFiles.SelectedText += text + Environment.NewLine;
                RtbCheckFiles.ScrollToCaret();
                SetCursorDown(RtbCheckFiles);
            }
        }

        private void BtnClearRenamedRtb_Click(object sender, EventArgs e)
        {
            RtbRenamedText.Clear();
        }

        private void BtnClearCheckFilesRtb_Click(object sender, EventArgs e)
        {
            RtbCheckFiles.Clear();
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

        private void SetCursorDown(RichTextBox Rtb)
        {
            Rtb.SelectionStart = Rtb.Text.Length;
            Rtb.Focus();

        }
    }

}
