using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace kensastr
{
    public partial class Form1 : Form
    {
        static DataTable dataTable;
        public Form1()
        {
            InitializeComponent();


        }

        //https://www.oborodukiyo.info/Forms/VS2008/F-ReadCSVToDataTable
        //dt:データを入れるDataTable
        //hasHeader:CSVの一行目がカラム名かどうか
        //fileName:ファイル名
        //separator:カラムを分けている文字(,など)
        //quote:カラムを囲んでいる文字("など)
        private void ReadCSV(DataTable dt, bool hasHeader, string fileName, string separator, bool quote)
        {
            //CSVを便利に読み込んでくれるTextFieldParserを使います。
            TextFieldParser parser = new TextFieldParser(fileName, Encoding.GetEncoding("shift_jis"));
            //これは可変長のフィールドでフィールドの区切りのマーカーが使われている場合です。
            //フィールドが固定長の場合は
            //parser.TextFieldType = FieldType.FixedWidth;
            parser.TextFieldType = FieldType.Delimited;
            //区切り文字を設定します。
            parser.SetDelimiters(separator);
            //クォーテーションがあるかどうか。
            //但しダブルクォーテーションにしか対応していません。シングルクォーテーションは認識しません。
            parser.HasFieldsEnclosedInQuotes = quote;
            string[] data;
            //ここのif文では、DataTableに必要なカラムを追加するために最初に1行だけ読み込んでいます。
            //データがあるか確認します。
            if (!parser.EndOfData)
            {
                //CSVファイルから1行読み取ります。
                data = parser.ReadFields();
                //カラムの数を取得します。
                int cols = data.Length;
                if (hasHeader)
                {
                    for (int i = 0; i < cols; i++)
                    {
                        dt.Columns.Add(new DataColumn(data[i]));
                    }
                }
                else
                {
                    for (int i = 0; i < cols; i++)
                    {
                        //カラム名にダミーを設定します。
                        dt.Columns.Add(new DataColumn());
                    }
                    //DataTableに追加するための新規行を取得します。
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < cols; i++)
                    {
                        //カラムの数だけデータをうつします。
                        row[i] = data[i];
                    }
                    //DataTableに追加します。
                    dt.Rows.Add(row);
                }
            }
            //ここのループがCSVを読み込むメインの処理です。
            //内容は先ほどとほとんど一緒です。
            while (!parser.EndOfData)
            {
                data = parser.ReadFields();
                DataRow row = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = data[i];
                }
                dt.Rows.Add(row);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataTable = new DataTable();

            Form1 form1 = new Form1();
            form1.ReadCSV(dataTable, false, "master.csv", ",", false);
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataTable = (DataTable)dataGridView1.DataSource;

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][1].ToString() != "")
                {
                    stringBuilder.Append(dataTable.Rows[i][0]);
                    stringBuilder.Append("");
                    stringBuilder.Append(dataTable.Rows[i][1]);
                    stringBuilder.Append(dataTable.Rows[i][2]);
                    stringBuilder.Append(",");
                }
            }

            string str = stringBuilder.ToString();
            this.textBox1.Text = str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataTable = (DataTable)dataGridView1.DataSource;

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][1].ToString() != "")
                {
                    stringBuilder.Append(dataTable.Rows[i][0]);
                    stringBuilder.Append("");
                    stringBuilder.Append(dataTable.Rows[i][1]);
                    stringBuilder.Append(dataTable.Rows[i][2]);
                    stringBuilder.Append("\r\n");
                }

            }
            string str = stringBuilder.ToString();
            this.textBox1.Text = str;

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.textBox1.Text);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("help.html");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
