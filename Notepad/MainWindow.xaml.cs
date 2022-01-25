/*
 * Name         : Sunraj Sharma
 * Programm     : Windows and Moblie Programming 
 * File         : MainWindow.xaml.cs
 * Date         : 25 September; 2021
 * Description  : This File contains different functions that are used in notepad i.e. Save, Open, New, Close and About.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32; //added for using FileDialog and Filter
using System.IO;    //added for save and open related things

/***********CITATIONS: used in subfeatures mostly *****************/
//https://docs.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-5.0 -> writeAllText (Microsoft doc)
//https://docs.microsoft.com/en-us/dotnet/api/system.windows.messageboxresult?view=net-5.0 -> MessageBoxResult (Microsoft doc)
//https://stackoverflow.com/questions/1252613/handling-cancel-button-in-yes-no-cancel-messagebox-in-formclosing-method -> MessageBoxResult (Stack overflow)
//https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.savefiledialog?view=net-5.0 -> saveFileDialog (Microsoft doc)
//https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.filedialog.filter?view=net-5.0 -> Filter (Microsoft doc)

namespace A_02_notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private String name;
        /*
         * Function     : public String mame
         * Parameter    : none        
         * Return       : none
         * Description  : get set function getting name of files
         */
        public String mame
        {
            get { return name; }
            set { name = value; }
        }

        private String path;
        /*
         * Function     : private String path;
         * Parameter    : none        
         * Return       : none
         * Description  : get set function for getting path 
         */
        public String Path
        {
            get { return path; }
            set { path = value; }
        }

        private String words;
        /*
         * Function     : private String words;
         * Parameter    : none        
         * Return       : none
         * Description  : get set function for words in text editing area
         */
        public String Words
        {
            get { return words; }
            set { words = value; }
        }

        private int count;
        /*
         * Function     : private int count;
         * Parameter    : none        
         * Return       : none
         * Description  : get set function
         */
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        /*
         * Function     : public bool checkSave
         * Parameter    : none        
         * Return       : bool
         * Description  : checks for things required to save a file, and is used in new file 
         */
        public bool checkSave()
        {
                return (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Path));  
        }

        /*
         * Function     : private void calculate_words
         * Parameter    : object sender, RoutedEventArgs e
         * Return       : none
         * Description  : calculates word count
         */
        private void calculate_words(object sender, RoutedEventArgs e)
        {
            Words = editor.Text;
            Count = Words.Length;
            word_count.Text = "Word Count: " + Count;
        }

        /*
         * Function     : private void new_Btn_Click   -> for File -> New
         * Parameter    : object sender, RoutedEventArgs e
         * Return       : void
         * Description  : for creating new document. Basically checks for text on edior, if finds something then asks for saving. When saved it returns a new file
         *                basically erasing everything of off editor to provide a clean screen
         */
        private void new_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Count == 0 && checkSave()) //checks if there is something in editor.text if nothing then it moves inside if
            {
                mame = String.Empty;
                Path = String.Empty;
                Words = String.Empty;
            }
            else // If some file in that moment already have content on it, Else it will ask for save the file
            {
                MessageBoxResult saveInfo = MessageBox.Show("Save changes", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (saveInfo)
                {
                    case MessageBoxResult.Yes:
                        {
                            var saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                Path = saveFileDialog.FileName;
                                Name = saveFileDialog.SafeFileName;
                                File.WriteAllText(Path, Words);
                            }
                            break;
                        }
                    case MessageBoxResult.No:
                        {
                            editor.Text = "";
                            break;
                        }
                }
            }
        }
        /*
        * Function     : private void open_Btn_Click    -> for File-> open
        * Parameter    : object sender, RoutedEventArgs e
        * Return       : void
        * Description  : 
        */
        private void open_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Count ==0 && checkSave())
            {
                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog()==true)
                {
                    Path = openFileDialog.FileName;
                    mame = openFileDialog.SafeFileName;
                    Words = File.ReadAllText(Path); //reads line from the selected file
                    editor.Text = File.ReadAllText(Path); //puts them on editor
                }
            }

            else // If some file in that moment already have content on it, Else it will ask for save the file 
            {
                MessageBoxResult saveInfo = MessageBox.Show("Save changes", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (saveInfo)
                {
                    case MessageBoxResult.Yes:
                        {
                            var saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";

                            if (saveFileDialog.ShowDialog() == true)
                            {
                                Path = saveFileDialog.FileName;
                                Name = saveFileDialog.SafeFileName;
                                File.WriteAllText(Path, Words);
                            }
                            break;
                        }
                    case MessageBoxResult.No: //when no continue the code in above if statement
                        { 
                            editor.Text = ""; //empty text editor, and then show dialog box
                        } 

                    var openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        Path = openFileDialog.FileName;
                        mame = openFileDialog.SafeFileName;
                        Words = File.ReadAllText(Path); //reads line from the selected file
                        editor.Text = File.ReadAllText(Path); //puts them on editor
                    }       
                    break;
                }
            }
        }

        /*
        * Function     : private void saveAs_Btn_Click -> for File -> save As
        * Parameter    : object sender, RoutedEventArgs e
        * Return       : void
        * Description  : Saving the stuff written in the editor at that moment
        */
        private void saveAs_Btn_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                Path = saveFileDialog.FileName;
                mame = saveFileDialog.SafeFileName;
                File.WriteAllText(Path, Words);
            }
        }

        /*
        * Function     : private void close_Btn_Click -> for File -> Close
        * Parameter    : object sender, RoutedEventArgs e
        * Return       : void
        * Description  : Gives a chance to save 
        */
        private void close_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult saveInfo = MessageBox.Show("Save changes", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (saveInfo)
            {
                case MessageBoxResult.Yes:
                    {
                        var saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            Path = saveFileDialog.FileName;
                            Name = saveFileDialog.SafeFileName;
                            File.WriteAllText(Path, Words);
                        }
                        break;
                    }
                case MessageBoxResult.No: //when no continue the code in above if statement
                    {
                        Close();
                        break;
                    }
                case MessageBoxResult.Cancel:
                    {
                        break;
                    }
            }  
        }

        /*
        * Function     : private void about_Btn_Click -> for Help -> about
        * Parameter    : object sender, RoutedEventArgs e
        * Return       : void
        * Description  : Gives a chance to save 
        */
        private void about_Btn_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}