using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        private XmlDocument xmlDoc;
        private string filePath = "data.xml";
        public Form1()
        {
            InitializeComponent();
            InitializeXml();
            LoadXmlData();
        }
        private void InitializeXml()
        {
            xmlDoc = new XmlDocument();
            if (!System.IO.File.Exists(filePath))
            {
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDoc.DocumentElement;
                xmlDoc.InsertBefore(xmlDeclaration, root);
                XmlElement employeesNode = xmlDoc.CreateElement("Employees");
                xmlDoc.AppendChild(employeesNode);
                xmlDoc.Save(filePath);
            }
            else
            {
                xmlDoc.Load(filePath);
            }
        }

        private void LoadXmlData()
        {
            dataGridView1.Rows.Clear();
            foreach (XmlNode node in xmlDoc.SelectNodes("//Employee"))
            {
                string name = node.SelectSingleNode("Name").InnerText;
                string age = node.SelectSingleNode("Age").InnerText;
                string position = node.SelectSingleNode("Position").InnerText;
                string department = node.SelectSingleNode("Department").InnerText;
                dataGridView1.Rows.Add(name, age, position, department);
            }
        }

        private void SaveXmlData()
        {
            xmlDoc.Save(filePath);
        }

        private void AddEmployee(string name, string age, string position, string department)
        {
            XmlNode employeesNode = xmlDoc.SelectSingleNode("//Employees");
            XmlElement employeeNode = xmlDoc.CreateElement("Employee");

            XmlElement nameNode = xmlDoc.CreateElement("Name");
            nameNode.InnerText = name;
            employeeNode.AppendChild(nameNode);

            XmlElement ageNode = xmlDoc.CreateElement("Age");
            ageNode.InnerText = age;
            employeeNode.AppendChild(ageNode);

            XmlElement positionNode = xmlDoc.CreateElement("Position");
            positionNode.InnerText = position;
            employeeNode.AppendChild(positionNode);

            XmlElement departmentNode = xmlDoc.CreateElement("Department");
            departmentNode.InnerText = department;
            employeeNode.AppendChild(departmentNode);

            employeesNode.AppendChild(employeeNode);
        }

        private void UpdateEmployee(int rowIndex, string name, string age, string position, string department)
        {
            XmlNodeList employeesNodes = xmlDoc.SelectNodes("//Employee");
            XmlNode employeeNode = employeesNodes[rowIndex];

            employeeNode.SelectSingleNode("Name").InnerText = name;
            employeeNode.SelectSingleNode("Age").InnerText = age;
            employeeNode.SelectSingleNode("Position").InnerText = position;
            employeeNode.SelectSingleNode("Department").InnerText = department;
        }

        private void DeleteEmployee(int rowIndex)
        {
            XmlNodeList employeesNodes = xmlDoc.SelectNodes("//Employee");
            XmlNode employeeNode = employeesNodes[rowIndex];
            employeeNode.ParentNode.RemoveChild(employeeNode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployee(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text);
            LoadXmlData();
            SaveXmlData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                UpdateEmployee(rowIndex, textBox1.Text, textBox2.Text,textBox3.Text, comboBox1.Text);
                LoadXmlData();
                SaveXmlData();
            }
            else
            {
                MessageBox.Show("Виберіть рядок для оновлення.", "Помилка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                DeleteEmployee(rowIndex);
                LoadXmlData();
                SaveXmlData();
            }
            else
            {
                MessageBox.Show("Виберіть рядок для оновлення.", "Помилка");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблиця пуста.", "Помилка.");
            }

        }

     
        }
    }
