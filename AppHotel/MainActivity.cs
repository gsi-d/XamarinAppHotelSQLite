using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using APPHotel;
using MySqlConnector;
using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.IO;

namespace AppHotel
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        string pasta_dados;
        string base_dados; //= "storage/emulated/0/Android/data/com.companyname.apphotel/files/PASTA_DADOS";

        Variaveis var = new Variaveis();
        //Conexao con = new Conexao();

        ImageView imgLogo, imgUser, imgSenha;
        EditText txtUser, txtSenha;
        Button btnLogin;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            
            imgLogo = FindViewById < ImageView > (Resource.Id.imgLogo);
            imgUser = FindViewById < ImageView > (Resource.Id.imgUser);
            imgSenha = FindViewById < ImageView > (Resource.Id.imgSenha);
            txtUser = FindViewById <EditText> (Resource.Id.txtUser);
            txtSenha = FindViewById <EditText> (Resource.Id.txtSenha);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            imgLogo.SetImageResource(Resource.Drawable.logo);
            imgSenha.SetImageResource(Resource.Drawable.senha);
            imgUser.SetImageResource(Resource.Drawable.usuarios);

            pasta_dados = Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).ToString(), "PASTA_DADOS");
            if (!Directory.Exists(pasta_dados))
            {
                Directory.CreateDirectory(pasta_dados);
            }
            base_dados = Path.Combine(pasta_dados, "base_dados.db");

            if (!File.Exists(base_dados))
            {
                SqliteConnection.CreateFile(base_dados);

                SqliteConnection con = new SqliteConnection("Data Source = " + base_dados + "; Version = 3");
                con.Open();

                SqliteCommand command = new SqliteCommand(con);
                command.CommandText = "CREATE TABLE cargos(" +
                                      "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                                      "cargo NVARCHAR(50) NOT NULL)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE detalhes_venda(" +
                                        "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                                        "id_venda INT NOT NULL," +
                                        "produto varchar NOT NULL," +
                                        "quantidade int NOT NULL," +
                                        "valor_unitario decimal(10, 2) NOT NULL," +
                                        "valor_total decimal(10, 2) NOT NULL," +
                                        "funcionario varchar NOT NULL," +
                                        "id_produto int NOT NULL," +
                                        "FOREIGN KEY (funcionario) REFERENCES funcionarios (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE fornecedores(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "nome varchar(40) NOT NULL," +
                  "endereco varchar(50) NOT NULL," +
                  "telefone varchar(20) NOT NULL)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE funcionarios(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "nome varchar(40) NOT NULL," +
                  "cpf varchar(20) NOT NULL," +
                  "endereco varchar(80) NOT NULL," +
                  "telefone varchar(20) NOT NULL," +
                  "cargo varchar(30) NOT NULL," +
                  "data date NOT NULL," +
                  "FOREIGN KEY (cargo) REFERENCES cargos (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE gastos(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "descricao varchar(60) NOT NULL," +
                  "valor decimal(10, 2) NOT NULL," +
                  "funcionario varchar(25) NOT NULL," +
                  "data date NOT NULL," +
                  "FOREIGN KEY (funcionario) REFERENCES funcionarios (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE hospedes(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "nome varchar(30) NOT NULL," +
                  "cpf varchar(20) NOT NULL," +
                  "endereco varchar(50) NOT NULL," +
                  "telefone varchar(20) NOT NULL," +
                  "funcionario varchar(30) NOT NULL," +
                  "data date NOT NULL," +
                  "FOREIGN KEY (funcionario) REFERENCES funcionarios (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE movimentacoes(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "tipo varchar(15) NOT NULL," +
                  "movimento varchar(25) NOT NULL," +
                  "valor decimal(10, 2) NOT NULL," +
                  "funcionario varchar(25) NOT NULL," +
                  "data date NOT NULL," +
                  "id_movimento int NOT NULL," +
                  "FOREIGN KEY (funcionario) REFERENCES funcionarios (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE novo_servico(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "hospede varchar(25) NOT NULL," +
                  "servico varchar(25) NOT NULL," +
                  "quarto varchar(10) NOT NULL," +
                  "valor decimal(10, 2) NOT NULL," +
                  "funcionario varchar(25) NOT NULL," +
                  "data date NOT NULL," +
                  "FOREIGN KEY (funcionario) REFERENCES funcionarios (id)," +
                  "FOREIGN KEY (servico) REFERENCES servicos (id)," +
                  "FOREIGN KEY (quarto) REFERENCES quartos (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE ocupacoes(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "quarto varchar(5) NOT NULL," +
                  "data date NOT NULL," +
                  "funcionario varchar(25) NOT NULL," +
                  "id_reserva varchar(5) NOT NULL DEFAULT '0'," +
                  "FOREIGN KEY (funcionario) REFERENCES funcionarios (id)," +
                  "FOREIGN KEY (id_reserva) REFERENCES reservas (id)," +
                  "FOREIGN KEY (quarto) REFERENCES quartos (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE reservas(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "quarto varchar(5) NOT NULL," +
                  "entrada date NOT NULL," +
                  "saida date NOT NULL," +
                  "dias int NOT NULL," +
                  "valor decimal(10, 2) NOT NULL," +
                  "nome varchar(25) NOT NULL," +
                  "telefone varchar(20) NOT NULL," +
                  "data date NOT NULL," +
                  "funcionario varchar(30) NOT NULL," +
                  "status varchar(15) NOT NULL DEFAULT 'Confirmada'," +
                  "checkin varchar(5) NOT NULL DEFAULT 'Não'," +
                  "checkout varchar(5) NOT NULL DEFAULT 'Não'," +
                  "pago varchar(5) NOT NULL DEFAULT 'Não'," +
                  "FOREIGN KEY (quarto) REFERENCES quartos (id)," +
                  "FOREIGN KEY (funcionario) REFERENCES funcionarios (id))";      
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE quartos(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "quarto varchar(5) NOT NULL," +
                  "valor decimal(10, 2) NOT NULL," +
                  "pessoas varchar(3) NOT NULL)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE  servicos(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "nome varchar(40) NOT NULL," +
                  "valor decimal(10, 2) NOT NULL)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE usuarios(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "nome varchar(30) NOT NULL," +
                  "cargo varchar(30) NOT NULL," +
                  "usuario varchar(30) NOT NULL," +
                  "senha varchar(30) NOT NULL," +
                  "data date NOT NULL)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE vendas(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "valor_total decimal(10, 2) NOT NULL," +
                  "funcionario varchar(40) NOT NULL," +
                  "status varchar(25) NOT NULL DEFAULT 'Efetuada'," +
                  "data date NOT NULL," +
                  "FOREIGN KEY (funcionario) REFERENCES funcionarios (id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE produtos(" +
                  "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                  "nome varchar(40) NOT NULL," +
                  "descricao varchar(80) NOT NULL," +
                  "estoque int NOT NULL DEFAULT '0'," +
                  "fornecedor int NOT NULL," +
                  "valor_venda decimal(10, 2) NOT NULL," +
                  "valor_compra decimal(10, 2) NOT NULL DEFAULT '0.00'," +
                  "data date NOT NULL," +
                  "FOREIGN KEY (fornecedor) REFERENCES fornecedores(id))";
                command.ExecuteNonQuery();

                command.CommandText =
                "INSERT INTO cargos(id, cargo) VALUES (10, 'Manobrista'),(11, 'Cozinheiro'),(12, 'Camareira'),(13, 'Garçom'),(14, 'Recepcionista'),(15, 'Gerente'),(16, 'Tesoureiro')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO detalhes_venda(id, id_venda, produto, quantidade, valor_unitario, valor_total, funcionario, id_produto) VALUES" +
                "(54, 10, 'Agua Mineral', 3, 5.00, 15.00, 'Hugo Freitas', 4)," +
                "(55, 10, 'Refrigerante Lata', 2, 5.50, 11.00, 'Hugo Freitas', 1)," +
                "(56, 11, 'Chocolate Barra', 1, 9.00, 9.00, 'Hugo Freitas', 3)," +
                "(57, 11, 'Refrigerante Lata', 6, 5.50, 33.00, 'Hugo Freitas', 1)," +
                "(58, 12, 'Cereal Barra', 1, 4.00, 4.00, 'Hugo Freitas', 5)," +
                "(59, 12, 'Cerveja Lata', 2, 8.00, 16.00, 'Hugo Freitas', 2)," +
                "(60, 13, 'Refrigerante Lata', 6, 5.50, 33.00, 'Hugo Freitas', 1)," +
                "(62, 15, 'Cerveja Lata', 3, 8.00, 24.00, 'Hugo Freitas', 2)," +
                "(63, 15, 'Refrigerante Lata', 2, 5.50, 11.00, 'Hugo Freitas', 1)," +
                "(65, 15, 'Chocolate Barra', 1, 9.00, 9.00, 'Hugo Freitas', 3)," +
                "(66, 16, 'Cerveja Lata', 3, 8.00, 24.00, 'Hugo Freitas', 2)," +
                "(67, 16, 'Refrigerante Lata', 2, 5.50, 11.00, 'Hugo Freitas', 1)," +
                "(69, 18, 'Cerveja Lata', 5, 8.00, 40.00, 'Hugo Freitas', 2)," +
                "(70, 19, 'Cerveja Lata', 3, 8.00, 24.00, 'Hugo Freitas', 2)," +
                "(71, 20, 'Cerveja Lata', 5, 8.00, 40.00, 'Hugo Freitas', 2)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO fornecedores(id, nome, endereco, telefone) VALUES" +
                "(1, 'Paula Campos', 'Rua 650', '(56) 56656-5656')," +
                "(2, 'Marcela', 'Rua 9', '(89) 65656-5656')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO funcionarios(id, nome, cpf, endereco, telefone, cargo, data) VALUES" +
                "(1, 'Marcos Pedro', '111.111.111-11', 'Rua A', '(65) 22222-2222', 'Manobrista', '2019-05-06 00:00:00')," +
                "(2, 'Marcela', '266.565.656-56', 'Rua C', '(55) 55555-5555', 'Recepcionista', '2019-05-06 00:00:00')," +
                "(4, 'Hugo', '121.212.121-21', 'Rua A', '(55) 54545-4560', 'Gerente', '2019-05-06 00:00:00')," +
                "(5, 'Pedro', '123.659.999-99', 'Rua 5', '(56) 22222-2222', 'Manobrista', '2019-05-06 00:00:00')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO gastos(id, descricao, valor, funcionario, data) VALUES" +
                "(1, 'Compra de Produtos', 30.00, 'Hugo Freitas', '2019-05-13 00:00:00')," +
                "(2, 'Gasto com Cadeira', 90.00, 'Hugo Freitas', '2019-05-13 00:00:00')," +
                "(5, 'Concerto de TV', 450.00, 'Hugo Freitas', '2019-05-14 00:00:00')," +
                "(6, 'Compra de Produtos', 30.00, 'Hugo Freitas', '2019-05-20 00:00:00')," +
                "(7, 'Compra de Mesas', 600.00, 'Hugo Freitas', '2019-05-21 00:00:00')," +
                "(8, 'Compra de Produtos', 40.00, 'Hugo Freitas', '2019-05-21 00:00:00')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO hospedes(id, nome, cpf, endereco, telefone, funcionario, data) VALUES" +
                "(1, 'Marcela', '000.002.222-22', 'Rua', '(55) 55555-5555', 'Hugo Freitas', '2019-05-13 00:00:00')," +
                "(2, 'Paola', '111.111.111-11', 'Rua 5', '(89) 55555-5555', 'Hugo Freitas', '2019-05-13 00:00:00')," +
                "(3, 'Matheus', '222.222.222-22', 'Rua 10', '(66) 66666-6333', 'Hugo Freitas', '2019-05-13 00:00:00')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO movimentacoes(id, tipo, movimento, valor, funcionario, data, id_movimento) VALUES" +
                "(4, 'Saída', 'Gasto', 90.00, 'Hugo Freitas', '2019-05-13 00:00:00', 2)," +
                "(7, 'Entrada', 'Venda', 40.00, 'Hugo Freitas', '2019-05-13 00:00:00', 18)," +
                "(8, 'Entrada', 'Serviço', 150.00, 'Hugo Freitas', '2019-05-14 00:00:00', 1)," +
                "(11, 'Entrada', 'Serviço', 150.00, 'Hugo Freitas', '2019-05-14 00:00:00', 4)," +
                "(12, 'Entrada', 'Serviço', 90.00, 'Hugo Freitas', '2019-05-14 00:00:00', 5)," +
                "(13, 'Saída', 'Gasto', 450.00, 'Hugo Freitas', '2019-05-14 00:00:00', 5)," +
                "(14, 'Entrada', 'Serviço', 90.00, 'Hugo Freitas', '2019-05-14 00:00:00', 6)," +
                "(15, 'Entrada', 'Reserva', 450.00, 'Hugo Freitas', '2019-05-16 00:00:00', 5)," +
                "(16, 'Entrada', 'Reserva', 450.00, 'Hugo Freitas', '2019-05-16 00:00:00', 7)," +
                "(17, 'Entrada', 'Reserva', 900.00, 'Hugo Freitas', '2019-05-16 00:00:00', 9)," +
                "(18, 'Entrada', 'Reserva', 450.00, 'Hugo Freitas', '2019-05-16 00:00:00', 10)," +
                "(19, 'Entrada', 'Reserva', 1000.00, 'Hugo Freitas', '2019-05-20 00:00:00', 17)," +
                "(20, 'Entrada', 'Reserva', 450.00, 'Hugo Freitas', '2019-05-20 00:00:00', 18)," +
                "(21, 'Entrada', 'Reserva', 600.00, 'Hugo Freitas', '2019-05-20 00:00:00', 19)," +
                "(22, 'Saída', 'Gasto', 30.00, 'Hugo Freitas', '2019-05-20 00:00:00', 6)," +
                "(23, 'Entrada', 'Reserva', 450.00, 'Hugo Freitas', '2019-05-20 00:00:00', 22)," +
                "(24, 'Entrada', 'Reserva', 450.00, 'Hugo Freitas', '2019-05-21 00:00:00', 25)," +
                "(25, 'Entrada', 'Venda', 24.00, 'Hugo Freitas', '2019-05-21 00:00:00', 19)," +
                "(26, 'Saída', 'Gasto', 600.00, 'Hugo Freitas', '2019-05-21 00:00:00', 7)," +
                "(27, 'Entrada', 'Reserva', 400.00, 'Hugo Freitas', '2019-05-21 00:00:00', 28)," +
                "(28, 'Entrada', 'Venda', 40.00, 'Hugo Freitas', '2019-05-21 00:00:00', 20)," +
                "(29, 'Saída', 'Gasto', 40.00, 'Hugo Freitas', '2019-05-21 00:00:00', 8)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO novo_servico(id, hospede, servico, quarto, valor, funcionario, data) VALUES" +
                "(1, 'Paola', 'Cabelereira', '101', 150.00, 'Hugo Freitas', '2019-05-12 00:00:00')," +
                "(4, 'Matheus', 'Personal Trainner', '101', 150.00, 'Hugo Freitas', '2019-05-14 00:00:00')," +
                "(5, 'Paola', 'Massagem', '203', 90.00, 'Hugo Freitas', '2019-05-14 00:00:00')," +
                "(6, 'Matheus', 'Massagem', '302', 90.00, 'Hugo Freitas', '2019-05-14 00:00:00')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO ocupacoes(id, quarto, data, funcionario, id_reserva) VALUES" +
                "(150, '101', '2019-05-17 00:00:00', 'Hugo Freitas', '20')," +
                "(151, '101', '2019-05-18 00:00:00', 'Hugo Freitas', '20')," +
                "(152, '101', '2019-05-19 00:00:00', 'Hugo Freitas', '20')," +
                "(153, '101', '2019-05-20 00:00:00', 'Hugo Freitas', '20')," +
                "(154, '101', '2019-05-01 00:00:00', 'Hugo Freitas', '21')," +
                "(155, '101', '2019-05-02 00:00:00', 'Hugo Freitas', '21')," +
                "(156, '101', '2019-05-03 00:00:00', 'Hugo Freitas', '21')," +
                "(157, '103', '2019-05-20 00:00:00', 'Hugo Freitas', '22')," +
                "(158, '103', '2019-05-21 00:00:00', 'Hugo Freitas', '22')," +
                "(159, '103', '2019-05-22 00:00:00', 'Hugo Freitas', '22')," +
                "(166, '202', '2019-05-20 00:00:00', 'Hugo Freitas', '24')," +
                "(167, '101', '2019-05-21 00:00:00', 'Hugo Freitas', '25')," +
                "(168, '101', '2019-05-22 00:00:00', 'Hugo Freitas', '25')," +
                "(169, '101', '2019-05-23 00:00:00', 'Hugo Freitas', '25')," + 
                "(173, '101', '2019-05-26 00:00:00', 'Hugo Freitas', '27')," +
                "(174, '101', '2019-05-27 00:00:00', 'Hugo Freitas', '27')," + 
                "(175, '101', '2019-05-28 00:00:00', 'Hugo Freitas', '27')," + 
                "(176, '101', '2019-05-29 00:00:00', 'Hugo Freitas', '27')," +
                "(177, '202', '2019-05-21 00:00:00', 'Hugo Freitas', '28')," +
                "(178, '202', '2019-05-22 00:00:00', 'Hugo Freitas', '28')";
                command.ExecuteNonQuery();

                

                command.CommandText = "INSERT INTO quartos(id, quarto, valor, pessoas) VALUES" +
                "(1, '101', 150.00, '2')," +
                "(2, '102', 150.00, '2')," +
                "(3, '103', 150.00, '2')," +
                "(4, '201', 200.00, '3')," + 
                "(5, '202', 200.00, '3')," + 
                "(6, '203', 200.00, '3')," +
                "(7, '301', 300.00, '2')," +
                "(8, '302', 300.00, '2')," +
                "(9, '303', 450.00, '2')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO reservas(id, quarto, entrada, saida, dias, valor, nome, telefone, data, funcionario, status, checkin, checkout, pago) VALUES" +
                "(18, '101', '2019-05-18 00:00:00', '2019-05-20 00:00:00', 3, 450.00, 'Paloma', '(22) 22222-2222', '2019-05-20 00:00:00', 'Hugo Freitas', 'Confirmada', 'Sim', 'Sim', 'Sim')," +
                "(20, '101', '2019-05-17 00:00:00', '2019-05-20 00:00:00', 4, 600.00, 'Marcelo', '(22) 65656-5656', '2019-05-20 00:00:00', 'Hugo Freitas', 'Confirmada', 'Não', 'Não', 'Não')," +
                "(21, '101', '2019-05-01 00:00:00', '2019-05-03 00:00:00', 3, 450.00, 'Paloma', '(11) 58989-8984', '2019-05-20 00:00:00', 'Hugo Freitas', 'Confirmada', 'Não', 'Não', 'Não')," +
                "(22, '103', '2019-05-20 00:00:00', '2019-05-22 00:00:00', 3, 450.00, 'Francisco', '(25) 89898-9899', '2019-05-20 00:00:00', 'Hugo Freitas', 'Confirmada', 'Sim', 'Não', 'Sim')," +
                "(23, '201', '2019-05-20 00:00:00', '2019-05-24 00:00:00', 5, 1000.00, 'Matheus', '(99) 88989-8989', '2019-05-20 00:00:00', 'Hugo Freitas', 'Confirmada', 'Não', 'Sim', 'Não')," +
                "(25, '101', '2019-05-21 00:00:00', '2019-05-23 00:00:00', 3, 450.00, 'Paulo', '(22) 22222-2222', '2019-05-21 00:00:00', 'Hugo Freitas', 'Confirmada', 'Sim', 'Não', 'Sim')," +
                "(26, '102', '2019-05-19 00:00:00', '2019-05-21 00:00:00', 3, 450.00, 'Marcos', '(22) 22222-2222', '2019-05-21 00:00:00', 'Hugo Freitas', 'Confirmada', 'Não', 'Sim', 'Não')," +
                "(27, '101', '2019-05-26 00:00:00', '2019-05-29 00:00:00', 4, 600.00, 'Thiago', '(55) 65656-6565', '2019-05-21 00:00:00', 'Hugo Freitas', 'Confirmada', 'Não', 'Não', 'Não')," +
                "(28, '202', '2019-05-21 00:00:00', '2019-05-22 00:00:00', 2, 400.00, 'Marcel', '(22) 22222-2222', '2019-05-21 00:00:00', 'Hugo Freitas', 'Confirmada', 'Sim', 'Não', 'Sim')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO servicos(id, nome, valor) VALUES" +
                "(3, 'Cabelereira', 150.00)," +
                "(4, 'Massagem', 90.00)," +
                "(5, 'Personal Trainner', 75.00)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO usuarios(id, nome, cargo, usuario, senha, data) VALUES" +
                "(1, 'Marcos', 'Recepcionista', 'marcos', '123', '2019-05-06 00:00:00')," +
                "(2, 'Hugo Freitas', 'Gerente', 'hugo', '123', '2019-05-06 00:00:00')," +
                "(3, 'Guilherme', 'Estagiário', 'guilherme', '123', '2019-05-06 00:00:00')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO vendas(id, valor_total, funcionario, status, data) VALUES" +
                "(9, 31.50, 'Hugo Freitas', 'Cancelada', '2019-05-07 00:00:00')," +
                "(10, 26.00, 'Hugo Freitas', 'Efetuada', '2019-05-07 00:00:00')," +
                "(11, 42.00, 'Hugo Freitas', 'Efetuada', '2019-05-09 00:00:00')," +
                "(12, 20.00, 'Hugo Freitas', 'Efetuada', '2019-05-08 00:00:00')," +
                "(13, 33.00, 'Hugo Freitas', 'Efetuada', '2019-05-09 00:00:00')," +
                "(14, 32.00, 'Hugo Freitas', 'Cancelada', '2019-05-09 00:00:00')," +
                "(15, 44.00, 'Hugo Freitas', 'Efetuada', '2019-05-09 00:00:00')," +
                "(16, 35.00, 'Hugo Freitas', 'Efetuada', '2019-05-13 00:00:00')," +
                "(17, 10.00, 'Hugo Freitas', 'Cancelada', '2019-05-13 00:00:00')," +
                "(18, 40.00, 'Hugo Freitas', 'Efetuada', '2019-05-13 00:00:00')," +
                "(19, 24.00, 'Hugo Freitas', 'Efetuada', '2019-05-21 00:00:00')," +
                "(20, 40.00, 'Hugo Freitas', 'Efetuada', '2019-05-21 00:00:00')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO produtos(nome, descricao, estoque, fornecedor, valor_venda, valor_compra, data) VALUES" +
                "('Refrigerante Lata', 'Lata 350 ML', 23, 2, 5.50, 2.00, '2019-05-07 00:00:00'," +
                "('Cerveja Lata', 'Lata 350 ML', 31, 2, 8.00, 2.00, '2019-05-07 00:00:00'," +
                "('Chocolate Barra', 'Barra 175 Gramas', 20, 2, 9.00, 3.00, '2019-05-08 00:00:00'," +
                "('Agua Mineral', 'Garrafa 200 ML', 24, 2, 5.00, 3.00, '2019-05-08 00:00:00'," +
                "('Cereal Barra', 'Barra Cereal 80 G', 21, 2, 4.00, 1.50, '2019-05-08 00:00:00'," +
                "('Suco Caixinha', 'Caixa 200 ML', 15, 2, 5.00, 2.00, '2019-05-08 00:00:00'," +
                "('Suco Lata', 'Lata 350 ML', 20, 1, 6.00, 2.00, '2019-05-08 00:00:00')";
                command.ExecuteNonQuery();

                con.Close();
                con.Dispose();

            }
            btnLogin.Click += BtnEntrar_Click;
        }

        private void BtnEntrar_Click(object sender, System.EventArgs e)
        {
            verificaLogin();
        }

        private void verificaLogin()
        {
            SqliteConnection con = new SqliteConnection("Data Source = " + base_dados + "; Version = 3");
            con.Open();

            SqliteCommand command = new SqliteCommand(con);

            if (txtUser.Text.ToString().Trim() == "")
            {
                Toast.MakeText(Application.Context, "Insira o usuário", ToastLength.Long).Show();
                txtUser.Text = "";
                txtUser.RequestFocus();
                return;
            }

            if (txtSenha.Text.ToString().Trim() == "")
            {
                Toast.MakeText(Application.Context, "Preencha a senha!", ToastLength.Long).Show();
                txtSenha.Text = "";
                txtSenha.RequestFocus();
                return;
            }
            //AQUI VAI O CÓDIGO PARA O LOGIN
            SqliteDataReader reader;
            
            command.CommandText = "SELECT * FROM usuarios where usuario = @usuario and senha = @senha";
            command.Parameters.AddWithValue("@usuario", txtUser.Text);
            command.Parameters.AddWithValue("@senha", txtSenha.Text);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                //EXTRAINDO INFORMAÇÕES DA CONSULTA DO LOGIN
                while (reader.Read())
                {
                    //RECUPERAR OS DADOS DO USUÁRIO
                    var.userLogado = reader["nome"].ToString();
                    var.cargoUser = reader["cargo"].ToString();


                }

                //Toast.MakeText(Application.Context, "Login Efetuado com Sucesso!", ToastLength.Long).Show();

                //INTENT PARA PASSAR PARAMETROS ENTRE ACT

                var tela = new Intent(this, typeof(Menu));
                tela.PutExtra("nome", var.userLogado);
                tela.PutExtra("cargo", var.cargoUser);
                tela.PutExtra("conexao", base_dados);
                StartActivity(tela);
                Limpar();

            }
            else
            {
                Toast.MakeText(Application.Context, "Dados Incorretos!", ToastLength.Long).Show();
                Limpar();
            }

            con.Close();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void Limpar()
        {
            txtUser.Text = "";
            txtSenha.Text = "";
        }
    }
}