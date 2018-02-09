using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ConsoleApp1 {
    class Program {

        static void RunUpdate(String cloudVersionString) {
            string path = Environment.GetFolderPath( Environment.SpecialFolder.CommonDocuments) + "/localVersion.txt"; 
            string localVersionString = "localVersion " + cloudVersionString;
            File.WriteAllText( path, localVersionString );
        }//end RunUpdate();


        static Version GetCloudVersion() {
            string versionFromFileLocal = Properties.Resources.cloudVersion;
            List<string> wordsFromCloudFile = versionFromFileLocal.Split( new[] { " " }, StringSplitOptions.RemoveEmptyEntries ).ToList();
            string theLocalVersionStr = wordsFromCloudFile[1];
            var versionFromFile = new Version( "0.0.1" );
            if( !string.IsNullOrEmpty( theLocalVersionStr ) ) 
            {
                versionFromFile = new Version( theLocalVersionStr ); 
            }
            return versionFromFile;
        }//end GetCloudVersion()


        static Version GetLocalVersion() {
            Version versionFromFile = new Version("0.0.1");
            string localPublicDocsPathForLocalVersion = Environment.GetFolderPath( Environment.SpecialFolder.CommonDocuments) + "/localVersion.txt";
            if( File.Exists(localPublicDocsPathForLocalVersion))
            {
                StreamReader localVersionReader = new StreamReader(localPublicDocsPathForLocalVersion);
                string thelocalVer = localVersionReader.ReadLine();
                localVersionReader.Close();

                List<string> wordsFromLocalFile = thelocalVer.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string theLocalVersionStr = wordsFromLocalFile[1];
                if (!string.IsNullOrEmpty(theLocalVersionStr))
                {
                    versionFromFile = new Version(theLocalVersionStr);
                }
            }
            else {
                File.WriteAllText(localPublicDocsPathForLocalVersion, versionFromFile.ToString() );
            }
            return versionFromFile;
        }//end GetLocalVersion()


        static void Main( string[] args ) {
            Version cloudVersion;
            if( args.Length == 0 )
            {
                cloudVersion = GetCloudVersion();
            }
            else {
                cloudVersion = new Version(args[0]);
            }
            Version localVersion = GetLocalVersion();
            
            if ( cloudVersion.CompareTo( localVersion ) > 0 )
            {
                RunUpdate( cloudVersion.ToString() );
            }
            string versionString = "localVersion is: " + localVersion.ToString() + ".\ncloudVersion is: " + cloudVersion.ToString() + ".\napp version is build 65.44";
            Console.WriteLine( versionString );
            Console.ReadLine();
        }//end Main()
    }//end Program
}//end ConsoleApp1
