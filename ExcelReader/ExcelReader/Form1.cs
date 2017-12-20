using ExcelDataReader;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ExcelReader
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Excel Files(.xlsx)|*.xlsx";
            this.openFileDialog1.Title = "Select an excel file";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = openFileDialog1.FileName;
                FileStream stream = File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);                
                DataSet result = excelReader.AsDataSet();


                var people = new List<Person>();
                while (excelReader.Read())
                {
                    people.Add(new Person
                    {
                        FirstName = excelReader.GetString(0),
                        LastName = excelReader.GetString(1)
                    });
                }

                this.resultGrid.DataSource = people;
            }
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
