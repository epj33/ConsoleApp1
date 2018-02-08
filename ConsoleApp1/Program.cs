using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;


namespace ConsoleApp1 {
    class Program {

        static void RunUpdate(String cloudVersionString) {
            Console.WriteLine( "starting RunUpdate()" );
            string updateCloudString = "RunUpdate is updating to version " + cloudVersionString + "...";
            //string versionFilePath = @"../../localVersion.txt";
            string path = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + "/localVersion.txt"; 
            string localVersionString = "localVersion " + cloudVersionString;
            File.WriteAllText( path, localVersionString );
            //IResourceWriter localFileWriter = new ResourceWriter( Properties.Resources.localFile );
            //localFileWriter.AddResource( "localV", cloudVersionString );
            //localFileWriter.Generate();
            //localFileWriter.Close();
            //StreamWriter localVersionWriter = new StreamWriter( Properties.Resources.localFile );
            //localVersionWriter.WriteLine( "localVersion {0}", cloudVersionString );
            //localVersionWriter.Close();
            Console.WriteLine( updateCloudString );
        }//end RunUpdate();


        static Version GetCloudVersion() {
            //string versionFilePath = @"../../cloudVersioning.txt";
            string versionFromFileLocal = Properties.Resources.cloudVersion;
            List<string> wordsFromCloudFile = versionFromFileLocal.Split( new[] { " " }, StringSplitOptions.RemoveEmptyEntries ).ToList();
            string theLocalVersionStr = wordsFromCloudFile[1];
            //Console.WriteLine( "versionFromFileLocal: " + versionFromFileLocal );
            var versionFromFile = new Version( "0.0.0" );
            if( !string.IsNullOrEmpty( theLocalVersionStr ) ) 
            {
                //StreamReader versionFile = new StreamReader( versionFilePath );
                versionFromFile = new Version( theLocalVersionStr ); // versionFile.ReadLine() );
                //versionFile.Close();
            }
            return versionFromFile;
        }//end GetCloudVersion()


        static Version GetLocalVersion() {
            //string versionFilePath = @"../../localVersion.txt";
            //string versionFromFileLocal = Properties.Resources.localFile;
            //List<string> wordsFromLocalFile = versionFromFileLocal.Split( new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries ).ToList();
            string path = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + "/localVersion.txt";
            //get the string from the file.
            StreamReader localVersionReader = new StreamReader( path );
            string thelocalVer = localVersionReader.ReadLine();
            localVersionReader.Close();
            //string localVersionString = "localVersion " + cloudVersionString;
            List<string> wordsFromLocalFile = thelocalVer.Split( new[] { " " }, StringSplitOptions.RemoveEmptyEntries ).ToList();
            string theLocalVersionStr = wordsFromLocalFile[1];
            Console.WriteLine( "sub1: "+ wordsFromLocalFile[1] );
            //ResourceManager localResourceMgr = new ResourceManager( Properties.Resources.localFile );
            var versionFromFile = new Version( "0.0.0" );
            if( !string.IsNullOrEmpty( theLocalVersionStr ) ) 
            {
                //StreamReader versionFile = new StreamReader( versionFilePath );
                versionFromFile = new Version( theLocalVersionStr ); // versionFile.ReadLine() );versionFile.ReadLine() );
                //versionFile.Close();
            }
            else {
                Console.WriteLine( "empty file: " + versionFromFile.ToString() );
                File.WriteAllText( Properties.Resources.localFile, versionFromFile.ToString() );
            }
            Console.WriteLine( "versionFromFile for localVerison: " + theLocalVersionStr );
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
            string versionString = "localVersion is: " + localVersion.ToString() + "\ncloudVersion is: " + cloudVersion.ToString() +
                "\napp version is build 65.43";
            Console.WriteLine( versionString );
            Console.ReadLine();
        }//end Main()
    }//end Program
}//end ConsoleApp1
