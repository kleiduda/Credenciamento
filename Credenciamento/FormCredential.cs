using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Diagnostics;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Shapes;

namespace Credenciamento
{
    public partial class FormCredential : Form
    {
        private bool IsNew = true;
        public FormCredential()
        {
            InitializeComponent();
        }
        private void FormCredential_Load(object sender, EventArgs e)
        {
            listarCadastros();
            FormatarTabela();
        }
        public void Clean()
        {
            txtCpf.Clear();
            txtCnpj.Clear();
            txtNome.Clear();
            txtNomeFantasia.Clear();
            txtUf.Clear();
            txtCidade.Clear();

        }
        public void listarCadastros()
        {
            Comando.Connection = Con.OpenConection();
            Comando.CommandText = "SELECT * FROM cadastro";
            Comando.CommandType = CommandType.Text;

            SqlDataAdapter SqlDat = new SqlDataAdapter(Comando);
            DataTable DtResult = new DataTable();
            SqlDat.Fill(DtResult);
            dtCadastros.DataSource = DtResult;
        }
        //FORMATANDO TABELA
        public void FormatarTabela()
        {
            dtCadastros.Columns["id"].Visible = false;
            dtCadastros.Columns["estado"].Visible = false;
            dtCadastros.Columns["cidade"].Visible = false;
            dtCadastros.Columns["tipo"].Visible = false;

            dtCadastros.Columns["nome_fantasia"].HeaderText = "EMPRESA";
            dtCadastros.Columns["cnpj"].HeaderText = "CNPJ";
            dtCadastros.Columns["nome_cracha"].HeaderText = "NOME";
            dtCadastros.Columns["cpf"].HeaderText = "CPF";
        }

        //LISTANDO OS CADASTROS

        protected Connection Con = new Connection();
        protected SqlCommand Comando = new SqlCommand();
        protected SqlDataReader LerLinhas;
        private void btnCadastro_Click(object sender, EventArgs e)
        {

            Comando.Connection = Con.OpenConection();
            Comando.CommandText = "INSERT INTO cadastro (cnpj, nome_fantasia, estado, cidade, nome_cracha, cpf, tipo)" +
                                  "Values(@cnpj, @nome_fantasia, @estado, @cidade, @nome_cracha, @cpf, @tipo)";
            Comando.CommandType = CommandType.Text;

            Comando.Parameters.AddWithValue("@cnpj", SqlDbType.VarChar).Value = txtCnpj.Text;
            Comando.Parameters.AddWithValue("@nome_fantasia", SqlDbType.VarChar).Value = txtNomeFantasia.Text;
            Comando.Parameters.AddWithValue("@estado", SqlDbType.VarChar).Value = txtUf.Text;
            Comando.Parameters.AddWithValue("@cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            Comando.Parameters.AddWithValue("@nome_cracha", SqlDbType.VarChar).Value = txtNome.Text;
            Comando.Parameters.AddWithValue("@cpf", SqlDbType.VarChar).Value = txtCpf.Text;
            Comando.Parameters.AddWithValue("@tipo", SqlDbType.VarChar).Value = cbTipo.Text;

            Comando.ExecuteNonQuery();
            Comando.Parameters.Clear();
            Con.ClosedConection();

            MessageBox.Show("CADASTRO REALIZADO COM SUCESSO!");

            Clean();
            listarCadastros();
        }

    

//IMPRIMINDO O ARQUIVO PDF
public Document pdfCadastro()
{
    //novo dumento
    Document document = new Document();
    document.Info.Title = "Pdf Pedido";
    document.Info.Subject = "Estudo de geraçã de PDF com Migradoc e PdfSharp";
    document.Info.Author = "Kleiton Freitas";

    Section section1 = document.AddSection();
    section1.PageSetup.PageFormat = PageFormat.A4;
    //section1.PageSetup.TopMargin = "1cm";
    section1.PageSetup.LeftMargin = "0.5cm";
    section1.PageSetup.RightMargin = "0.5cm";

    TextFrame tfContainer = section1.AddTextFrame();
    tfContainer.Top = new Unit(0, UnitType.Centimeter);
    tfContainer.Left = new Unit(0, UnitType.Centimeter);
    tfContainer.Height = "11,8cm";
    tfContainer.WrapFormat.Style = WrapStyle.Through;
    tfContainer.LineFormat.Width = 0.1;
    tfContainer.Width = "7,5cm";
    tfContainer.MarginLeft = 0;
    tfContainer.MarginTop = 0;

    Paragraph data = tfContainer.AddParagraph();
    data.Format.Alignment = ParagraphAlignment.Center;
    data.Format.Font.Size = 25;
    data.AddFormattedText("03 a 06 FEV", TextFormat.Bold);

    tfContainer.AddParagraph();

    MigraDoc.DocumentObjectModel.Shapes.Image img = tfContainer.AddImage(@"C:\Users\Patricia Bastos\Desktop\CredPremier\Credenciamento\logo.jpg");
    img.Left = ShapePosition.Center;
    img.Width = 130;

    tfContainer.AddParagraph();
    tfContainer.AddParagraph();

    Paragraph nome = tfContainer.AddParagraph();
    nome.Format.Alignment = ParagraphAlignment.Center;
    nome.Format.Font.Size = 15;
    nome.AddFormattedText(dtCadastros.CurrentRow.Cells["nome_cracha"].Value.ToString(), TextFormat.Bold);

    Paragraph tipo = tfContainer.AddParagraph();
    tipo.Format.Alignment = ParagraphAlignment.Center;
    tipo.Format.Font.Size = 15;
    tipo.AddFormattedText(dtCadastros.CurrentRow.Cells["tipo"].Value.ToString(), TextFormat.Bold);

    tfContainer.AddParagraph();
    tfContainer.AddParagraph();

    Paragraph dadosEmpresa = tfContainer.AddParagraph();
    dadosEmpresa.Format.Alignment = ParagraphAlignment.Center;
    dadosEmpresa.Format.Font.Size = 15;
    dadosEmpresa.AddFormattedText("DADOS DA EMPRESA", TextFormat.Bold);

    tfContainer.AddParagraph();

    Paragraph cnpj = tfContainer.AddParagraph();
    cnpj.Format.Alignment = ParagraphAlignment.Center;
    cnpj.Format.Font.Size = 20;
    cnpj.AddFormattedText(dtCadastros.CurrentRow.Cells["cnpj"].Value.ToString(), TextFormat.Bold);

    Paragraph nomeFantasia = tfContainer.AddParagraph();
    nomeFantasia.Format.Alignment = ParagraphAlignment.Center;
    nomeFantasia.Format.Font.Size = 15;
    nomeFantasia.AddFormattedText(dtCadastros.CurrentRow.Cells["nome_fantasia"].Value.ToString(), TextFormat.Bold);

    Paragraph cidadeEstado = tfContainer.AddParagraph();
    cidadeEstado.Format.Alignment = ParagraphAlignment.Center;
    cidadeEstado.Format.Font.Size = 15;
    cidadeEstado.AddFormattedText(dtCadastros.CurrentRow.Cells["cidade"].Value.ToString() + "-" + dtCadastros.CurrentRow.Cells["estado"].Value.ToString());

    tfContainer.AddParagraph();
    tfContainer.AddParagraph();
    tfContainer.AddParagraph();

    Paragraph local = tfContainer.AddParagraph();
    local.Format.Alignment = ParagraphAlignment.Center;
    local.Format.Font.Size = 5;
    local.AddFormattedText("Local:\n", TextFormat.Bold);
    local.AddText("ESPAÇO IBIRAPUERA\n");
    local.AddText("AV IBIRAPUERA, 3.103 - PISO JURUPIS");


    //renderizando documento
    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

    //passando documento para o renderizador
    pdfRenderer.Document = document;


    //renderizador trabalhando
    pdfRenderer.RenderDocument();

    //salvando o PDF em um arquivo
    string fileName = dtCadastros.CurrentRow.Cells["nome_cracha"].Value.ToString() + ".pdf";
    pdfRenderer.PdfDocument.Save(fileName);


    System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
    processInfo.FileName = fileName;
    System.Diagnostics.Process.Start(processInfo);


    return document;


}

private void dtCadastros_DoubleClick(object sender, EventArgs e)
{
    pdfCadastro();
}
    }
}
