using System;
using System.IO;


namespace ConsoleApp1 {
    class Program {

        static void RunUpdate(String cloudVersionString) 
        {
            string updateCloudString = "RunUpdate is updating to version " + cloudVersionString + "...";
            string versionFilePath = @"c:\users\michaelstahle\documents\visual studio 2017\Projects\ConsoleApp1\ConsoleApp1\localVersion.txt";
            File.WriteAllText( versionFilePath, cloudVersionString );
            Console.WriteLine( updateCloudString );
        }//end RunUpdate();


        static Version GetCloudVersion() {
            string versionFilePath = @"c:\users\michaelstahle\documents\visual studio 2017\Projects\ConsoleApp1\ConsoleApp1\cloudVersion.txt";
            //string versionFilePathe = @"E:\eric\progFiles\jenkins\workspace\versionCheckFromGithub\luvCloudVersion.txt";
            var versionFromFile = new Version( "0.0.0" );
            if( File.Exists( versionFilePath ) )
            {
                //versionFromFile = new Version( File.ReadAllText( versionFilePath ) );//works. need better in case of more lines?
                StreamReader versionFile = new StreamReader( versionFilePath );
                versionFromFile = new Version( versionFile.ReadLine() );
                versionFile.Close();
            }
            return versionFromFile;
        }//end GetCloudVersion()


        static Version GetLocalVersion() 
        {
            string versionFilePath = @"c:\users\michaelstahle\documents\visual studio 2017\Projects\ConsoleApp1\ConsoleApp1\localVersion.txt";
            var versionFromFile = new Version( "0.0.0" );
            if( File.Exists( versionFilePath ) ) 
            {
                StreamReader versionFile = new StreamReader( versionFilePath );
                versionFromFile = new Version( versionFile.ReadLine() );
                versionFile.Close();
            }
            else {
                File.WriteAllText( versionFilePath, versionFromFile.ToString() );
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
            Console.WriteLine( "version is up to date!\nHit Enter to close." );
            //Console.ReadLine();
        }//end Main()
    }//end Program
}//end ConsoleApp1
