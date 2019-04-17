using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MetaDataMP3
{
    public partial class TagLib_Sharp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Delete all files in folder
            String[] strFilePath = Directory.GetFiles(@"D:\FormatedMusicFiles\");

            foreach (String filePath in strFilePath)
            {
                File.Delete(filePath);
            }

            //Set file path for original music files.
            String strOriginalMusicFiles = @"D:\OriginalMusicFiles\Technical Files";

            //Set the path to formated music files.
            String strFormatedMusicFiles = @"D:\FormatedMusicFiles\";

            //Get file path to each individual file.
            String[] filePaths = Directory.GetFiles(strOriginalMusicFiles, "*.mp3");

            //Main loop through filePaths array
            foreach (String files in filePaths)
            {

                //Create path to mp3 file.
                TagLib.File tagFile = TagLib.File.Create(files);

                //Create strings filled with the metadata from mp3 file. 
                String strFileName = Path.GetFileName(files);
                String strTitle = tagFile.Tag.Title;
                String strYear = Convert.ToString(tagFile.Tag.Year);

                //Save Original file name and build new file name with meta-data.
                String strOldName = strFileName;
                String strNewName = strTitle + " " + "(" + strYear + ")" + ".mp3";

                //Find and remove (:) from any Title Name if exists.
                if (strNewName.Contains(':'))
                {
                    strNewName = strNewName.Replace(':', '-');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (\) from any Title Name if exists.
                if (strNewName.Contains('/'))
                {
                    strNewName = strNewName.Replace('/', '#');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (*) from any Title Name if exists.
                if (strNewName.Contains('*'))
                {
                    strNewName = strNewName.Replace('*', '#');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (") from any Title Name if exists.
                if (strNewName.Contains('"'))
                {
                    strNewName = strNewName.Replace('"', '#');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (?) from any Title Name if exists.
                if (strNewName.Contains('?'))
                {
                    strNewName = strNewName.Replace('?', '#');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (<) from any Title Name if exists.
                if (strNewName.Contains('<'))
                {
                    strNewName = strNewName.Replace('<', '#');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (>) from any Title Name if exists.
                if (strNewName.Contains('>'))
                {
                    strNewName = strNewName.Replace('>', '#');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (|) from any Title Name if exists.
                if (strNewName.Contains('|'))
                {
                    strNewName = strNewName.Replace('|', '#');
                }
                else
                {
                    //Do nothing

                }

                //Find and remove (=) from any Title Name if exists.
                if (strNewName.Contains('='))
                {
                    strNewName = strNewName.Replace('=', ' ');
                }
                else
                {
                    //Do nothing

                }

                //Replace file name.
                String strChangedFileName = strOldName.Replace(strOldName, strNewName);

                //Remove old file name from file name Ex: 1864 The Qualities of True Love, Part 1 (1 Corinthians 13-4a-c)
                //Here you would remove '1864'
                //To do this find the number of characters in the orginal file excluding .mp3
                Int16 i16StartIndex = 0;
                Int16 i16Count = 0;

                //Get number of characters in orginal file name.
                i16Count = Convert.ToInt16(strFileName.Length - 4);

                //Now remove orginal file name to create new file name  Ex: "1864 The Qualities of True Love, Part 1 (1 Corinthians 13-4a-c)" --> 
                //"The Qualities of True Love, Part 1 (1 Corinthians 13-4a-c)"
                //Remove extra spaces from beginning of string
                String strFinalFileName = strChangedFileName.Remove(i16StartIndex, i16Count).ToString().Trim();

                //Finally, save a copy to hardrive.
                File.Copy(files, strFormatedMusicFiles + strFinalFileName);

            }

            //Get file path to each individual file in Formated Music Folder.
            String[] fileFormatedPaths = Directory.GetFiles(strFormatedMusicFiles, "*.mp3");

            //Loop through fileFormatedPaths array
            foreach (String filesFormated in fileFormatedPaths)
            {
                //Create path to mp3 file. (Formated Music Files)
                TagLib.File tagFileFormated = TagLib.File.Create(filesFormated);
                String strYear = Convert.ToString(tagFileFormated.Tag.Year);

                //Change File Created Date to Januray 1 and the year of the file already in meta-data
                String NewDateTime = "01/01/" + tagFileFormated.Tag.Year.ToString() + " 12:00:01 AM";
                File.SetCreationTime(filesFormated, Convert.ToDateTime(NewDateTime));
            }

        }

    }
}
