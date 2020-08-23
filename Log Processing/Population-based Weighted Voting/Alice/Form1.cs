using System;
using System.Xml.XPath;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alice
{
    public partial class Form1 : Form
    {
        public static string DIR = "C:\\Documents and Settings\\VAIO\\Desktop\\conceptBasedCombination\\";
        /// <summary>
        /// load form1 and initialize components
        /// </summary>
        ///------------->
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read the target text file and after preProcessing insert words in a SkipList
        /// </summary>
        //------------------------------------------------->
        private void Read_Click(object sender, EventArgs e)
        {
            //string DIR = adressLable.Text;

            StreamReader sr = new StreamReader(DIR + "perm.txt"); 
            string t = sr.ReadLine();
            int[] permutation = new int[1770];
            for (int k = 1; k <= 1; k++)
            {
                
                string[] components = t.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 1770; i++)
                    permutation[i] = Convert.ToInt32(components[i]);
            }


            int [][]indices = new int[1770][];
            int ctr = 0;
            for (int i = 0; i <60; i++)
            {
                
                for (int j = i + 1; j < 60; j++)
                {
                    indices[ctr] = new int[2];
                    indices[ctr][0] = i;
                    indices[ctr][1] = j; 
                    ctr++;
                }
            }

            int[][][] AccuracyBaseMatrix = new int[9][][];

            double[][][] M1BaseMatrix = new double[9][][];
            double[][][] M2BaseMatrix = new double[9][][];

            for (int p = 0; p < 9; p++)
            {
                sr = new StreamReader(DIR + "Base\\P"+(p+1)+"-Results_of_cross_validation.txt");
                sr.ReadLine(); // loop_count : accuracy : loop_time :  (i,j)   
                sr.ReadLine(); // ..............................................
                int counter = 0;
                AccuracyBaseMatrix[p] = new int[60][];
                M1BaseMatrix[p] = new double[60][];
                M2BaseMatrix[p] = new double[60][];
                for (int i = 0; i < 60; i++)
                {
                    AccuracyBaseMatrix[p][i] = new int[60];
                    M1BaseMatrix[p][i] = new double[60];
                    M2BaseMatrix[p][i] = new double[60];
                    for (int j = 0; j < 60; j++)
                    {
                        AccuracyBaseMatrix[p][i][j] = 0;
                        M1BaseMatrix[p][i][j] = 0;
                        M2BaseMatrix[p][i][j] = 0;
                    }//for
                }

                double accuracyPrev = 0.001;
                while (!sr.EndOfStream)
                {
                    counter++;
                    t = sr.ReadLine();
                    string[] components = t.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    double accuracyNow = Convert.ToDouble(components[1]);
                    int i = Convert.ToInt32(components[3]);
                    int j = Convert.ToInt32(components[4]);
                    M1BaseMatrix[p][i - 1][j - 1] = M1BaseMatrix[p][j - 1][i - 1] = Convert.ToDouble(components[5]);
                    M2BaseMatrix[p][i - 1][j - 1] = M2BaseMatrix[p][j - 1][i - 1] = Convert.ToDouble(components[6]); 

                    if (accuracyNow > accuracyPrev)
                        AccuracyBaseMatrix[p][i - 1][j - 1] = AccuracyBaseMatrix[p][j - 1][i - 1] = 1;
                    else
                        AccuracyBaseMatrix[p][i - 1][j - 1] = AccuracyBaseMatrix[p][j - 1][i - 1] = 0;

                    accuracyPrev = accuracyNow;
                }//while


                sr.Close();

            }//for Person

            int[][][] AccuracyLinMatrix = new int[9][][];
            double[][][] M1LinMatrix = new double[9][][];
            double[][][] M2LinMatrix = new double[9][][];
            for (int p = 0; p < 9; p++)
            {
                sr = new StreamReader(DIR + "LIN\\P" + (p+1) + "-Results_of_cross_validation.txt");
                sr.ReadLine(); // loop_count : accuracy : loop_time :  (i,j)   
                sr.ReadLine(); // ..............................................
                int counter = 0;
                AccuracyLinMatrix[p] = new int[60][];
                M1LinMatrix[p] = new double[60][];
                M2LinMatrix[p] = new double[60][];
                for (int i = 0; i < 60; i++)
                {
                    AccuracyLinMatrix[p][i] = new int[60];
                    M1LinMatrix[p][i] = new double[60];
                    M2LinMatrix[p][i] = new double[60];
                    for (int j = 0; j < 60; j++)
                    {
                        AccuracyLinMatrix[p][i][j] = 0;
                        M1LinMatrix[p][i][j] = 0;
                        M2LinMatrix[p][i][j] = 0;
                    }//for
                }

                double accuracyPrev = 0.001;
                while (!sr.EndOfStream)
                {
                    counter++;
                    t = sr.ReadLine();
                    string[] components = t.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    double accuracyNow = Convert.ToDouble(components[1]);
                    int i = Convert.ToInt32(components[3]);
                    int j = Convert.ToInt32(components[4]);

                    M1LinMatrix[p][i - 1][j - 1] = M1LinMatrix[p][j - 1][i - 1] = Convert.ToDouble(components[5]);
                    M2LinMatrix[p][i - 1][j - 1] = M2LinMatrix[p][j - 1][i - 1] = Convert.ToDouble(components[6]);

                    if (accuracyNow > accuracyPrev)
                        AccuracyLinMatrix[p][i - 1][j - 1] = AccuracyLinMatrix[p][j - 1][i - 1] = 1;
                    else
                        AccuracyLinMatrix[p][i - 1][j - 1] = AccuracyLinMatrix[p][j - 1][i - 1] = 0;

                    accuracyPrev = accuracyNow;
                }//while


                sr.Close();


                

            }//for Person

            double[][] PopulationBasedAccuracy = new double[9][];
            double[] accuracy = new double[9];
            Random random = new Random();

            for (int j = 0;j<9 ; j++)
            {
                PopulationBasedAccuracy[j] = new double[1770];
                for (int i = 0; i < 1770; i++)
                    PopulationBasedAccuracy[j][i] = 0;
            }//for

            for (int person = 0; person < 9; person++)
            {
                for (int pair = 0; pair < 1770; pair++)
                {
                    int i = indices[pair][0];
                    int j = indices[pair][1];
                    double m1Base = 0;
                    double m2Base = 0;
                    double m1Lin = 0;
                    double m2Lin = 0;

                    double RandomNumber = random.Next(0, 9);
                    while (RandomNumber == person)
                        RandomNumber = random.Next(0, 9);

                    int vote = 0;
                    for (int p = 0; p < 9; p++)
                    {

                        if (person == p || person == RandomNumber)
                           continue;

                        m1Base = M1BaseMatrix[p][i][j];
                        m2Base = M2BaseMatrix[p][i][j];
                        m1Lin = M1LinMatrix[p][i][j];      
                        m2Lin = M2LinMatrix[p][i][j];

                        bool cond1 = (m1Base > m2Base && m1Lin > m2Lin);
                        bool cond2 = (m1Base <= m2Base && m1Lin <= m2Lin);
                        if (cond1 || cond2)
                        {
                            if (m1Base - m2Base >= m1Lin - m2Lin)
                                vote++;
                        }
                        else if (m1Base > m2Base)
                            vote++;
                        


                        /*if (person == p)
                            continue;

                        m1Base += M1BaseMatrix[p][i][j];
                        m2Base += M2BaseMatrix[p][i][j];
                        m1Lin += M1LinMatrix[p][i][j];
                        m2Lin += M2LinMatrix[p][i][j];
                        */

                    }//for p not person!


                    //bool condition = vote > 3;                    //bool condition = m1Base >= m1Lin;
                    bool condition = true;//binary 
                    //bool condition = false;//correlated

                    double lamda = 0;
                    if (condition)
                    {
                        //lamda = ((double)vote)/8; // weighted
                        // for binary mode if the vote is more than 3 the base is winner
                        if (vote > 3)
                            lamda=1;
                        else
                            lamda=0;
                        double m1 = lamda * M1BaseMatrix[person][i][j] + (1 - lamda) * M1LinMatrix[person][i][j];
                        double m2 = lamda * M2BaseMatrix[person][i][j] + (1 - lamda) * M2LinMatrix[person][i][j];
                        int acc = 0;
                        if(m1>m2)
                            acc = 1;
                        PopulationBasedAccuracy[person][pair] = acc;
                        accuracy[person] += ((double)acc) / 1770;
                    }
                    else
                    {

                        double Dm1Base = m1Base - m2Base;
                        double Dm1Lin = m1Lin - m2Lin; 

                        if (Dm1Base < 0 && Dm1Lin < 0)
                        {
                            Dm1Base = 1/Math.Abs(Dm1Base);
                            Dm1Lin = 1 / Math.Abs(Dm1Lin);
                            lamda = Dm1Base / (Dm1Base + Dm1Lin);
                        }
                        else if (Dm1Base < 0)
                            lamda=0;
                        else if (Dm1Lin < 0)
                            lamda=1;
                        else
                            lamda = Dm1Base / (Dm1Base + Dm1Lin);
                        
                        if (lamda<0)
                        {
                            int danger = 1;
                            danger++;
                        }
                        double m1 = lamda * M1BaseMatrix[person][i][j] + (1 - lamda) * M1LinMatrix[person][i][j];
                        double m2 = lamda * M2BaseMatrix[person][i][j] + (1 - lamda) * M2LinMatrix[person][i][j];
                        int acc = 0;
                        if(m1>m2)
                            acc = 1;
                        PopulationBasedAccuracy[person][pair] = acc;
                        accuracy[person] += ((double)acc) / 1770;
                    }
                }//for
            }//for
                
            
            double average = 0;
            string result = "";
            for (int person = 0; person < 9; person++)
            {
                average += accuracy[person] / 9;
                result = result + ""+ accuracy[person]+ "\n";
            }

            result = result + " average = " + average;

            StreamWriter sw = new StreamWriter(DIR+"Result.txt");
            sw.WriteLine(result);
            sw.Close();



            int y = 0;
        

        }//Read
        /// <summary>
        /// import the text file from dialogue
        /// </summary>
        //--------------------------------------------------->
        private void import_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = adressLable.Text;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt"; //|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ShowDialog();
           
            adressLable.Text = openFileDialog1.FileName;
        }//import


        /// <summary>
        /// crawl the XML file
        /// </summary>
        //--------------------------------------------------->
        public static void crawl()
        {
            System.Console.WriteLine("Hi");

            string fileName = "rueters00to11.xml";
            string elementName = "/A/REUTERS/TOPICS/D";
            string result = GetNodeValue(fileName, elementName);
            System.Console.WriteLine("result is : " + result);
            System.Console.Read();
        }

        /// <summary>
        /// to get a special node value
        /// </summary>
        //--------------------------------------------------->
        public static string GetNodeValue(string sFileName, string sSelectExpression)
        {

            string sRetVal = "";
            // Read the XML document
            XPathDocument myXPathDocument = new XPathDocument(DIR + sFileName);
            XPathNavigator myXPathNavigator = myXPathDocument.CreateNavigator();
            XPathNodeIterator myXPathNodeIterator = myXPathNavigator.Select(sSelectExpression);
            while (myXPathNodeIterator.MoveNext())
            {
                //Console.WriteLine("<" + myXPathNodeIterator.Current.Name + "> " + myXPathNodeIterator.Current.Value);
                sRetVal = myXPathNodeIterator.Current.Value;
            }
            return sRetVal;
        }
    }//form
}//Alice
