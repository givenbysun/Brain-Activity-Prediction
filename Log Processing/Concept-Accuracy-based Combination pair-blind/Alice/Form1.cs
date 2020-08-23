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

            double[] ConceptBasedAccuracy = new double[9];
            for(int i=0;i<9;i++)
                ConceptBasedAccuracy[i] = 0;

            double[] GeneralBaseConAcc = new double[60];
            double[] GeneralLinContAcc = new double[60];
            for (int j = 0; j < 60; j++)
            {
                GeneralBaseConAcc[j] = 0;
                GeneralLinContAcc[j] = 0;
            }

            for (int j = 0; j < 1770; j++)
            {
                int I = indices[j][0];
                int J = indices[j][1];
                for (int p = 0; p < 9; p++)
                {
                    GeneralBaseConAcc[I] += ((double)AccuracyBaseMatrix[p][I][J])/9;
                    GeneralLinContAcc[I] += ((double)AccuracyLinMatrix[p][I][J])/9;
                    GeneralBaseConAcc[J] += ((double)AccuracyBaseMatrix[p][I][J])/9;
                    GeneralLinContAcc[J] += ((double)AccuracyLinMatrix[p][I][J]) / 9;
                }
            }//for

            for (int j = 0; j < 60; j++)
            {
                GeneralBaseConAcc[j] /= 59;
                GeneralLinContAcc[j] /= 59;
            }

            

            for (int p = 0; p < 9; p++)
            {
                for (int i = 0; i < 1770; i++)
                {
                    int It = indices[i][0];
                    int Jt = indices[i][1];

                    double[]BaseConAcc = new double[60];
                    double[]LinContAcc = new double[60];
                    double[]eventCounter = new double[60];
                    for (int j = 0; j < 60; j++)
                    {
                        BaseConAcc[j]=0;
                        LinContAcc[j]=0;
                        eventCounter[j] = 0;
                    }


                    //training
                    for (int j = 0; j < 1770; j++)
                    {
                        int index = permutation[j]-1;
                        int I = indices[index][0];
                        int J = indices[index][1];
                        if (I == It || J == Jt)
                            continue;
                        
                        eventCounter[I]++;
                        eventCounter[J]++;

                        LinContAcc[I]+=AccuracyLinMatrix[p][I][J];
                        BaseConAcc[I] += AccuracyBaseMatrix[p][I][J];

                        LinContAcc[J] += AccuracyLinMatrix[p][I][J];
                        BaseConAcc[J] += AccuracyBaseMatrix[p][I][J];
                    }//for      


                    for (int j = 0; j < 60; j++)
                    {
                        
                        if (eventCounter[j] != 0)
                        {
                            BaseConAcc[j] = BaseConAcc[j] / eventCounter[j];
                            LinContAcc[j] = LinContAcc[j] / eventCounter[j];
                        }//if
                    }//for



                    //test
                    double accuracy =0;

                    double accuracyPairLIN = 0;
                    double accuracyPairBase = 0;

                    for (int j = 0; j < 60; j++)
                    {

                        accuracyPairLIN += LinContAcc[j] / 58;
                        accuracyPairBase += BaseConAcc[j] / 58;

                    } 

                    int num = 0;
                    for (int j = 0; j < 60; j++)
                    {
                        for (int k = 0; k < 60; k++)
                        {
                            bool condition1 = j == k;
                            if (condition1)
                                continue;
                            bool condition2 = (j != It && j != Jt) ;
                            bool condition3 = (k != It && k != Jt);

                            if (condition2 && condition3)  
                            {
                                continue;
                            }//if 
                            num++;
                            int index = permutation[j] - 1;
                            int I = j;
                            int J = k;

                            double Landa = accuracyPairBase / (accuracyPairBase + accuracyPairLIN);
                            double m1 = Landa * M1BaseMatrix[p][I][J] + (1 - Landa) * M1LinMatrix[p][I][J];
                            double m2 = Landa * M2BaseMatrix[p][I][J] + (1 - Landa) * M2LinMatrix[p][I][J];

                            if ( m1>m2 ) //4 
                            {
                                accuracy++;
                            }
                        }//for
                   }//for
                    num = num / 2;
                    accuracy = accuracy / 2;    


                    accuracy = accuracy / (num);
                    ConceptBasedAccuracy[p] += (accuracy / 1770);
                }//for
            }//for people

            string result = "";
            double average = 0;
            for(int p=0;p<9;p++)
            {
                result += ConceptBasedAccuracy[p]+"\n";
                average += ConceptBasedAccuracy[p] / 9;
            }
            result += "Average = " + average ;

            result+="\n\n";
            double test1 = 0; double test2 = 0; double test3 = 0;
            for (int j = 0; j < 60; j++)
                    {
                            double bs =GeneralBaseConAcc[j] ;
                            double li =GeneralLinContAcc[j] ;
                            test1 += GeneralBaseConAcc[j];
                            test2 += GeneralBaseConAcc[j];
                result+="word "+(j+1)+" base "+bs+ " vs "+ " linLesk "+li+"\n";
                            //result +=  bs + " " + li + "\n";
                            

                    }//for

            test1 /= 60;
            test2 /= 60;
            test3 = 0.5*(test1+test2);

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
