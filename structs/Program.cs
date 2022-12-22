using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lista_de_clientes
{
    internal class Program
    {

        enum menu { Listagem = 1, Adicionar, Remover, sair };
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }
        static List<Cliente> clientes = new List<Cliente>();


        static void Main(string[] args)
        {
            Carregar();
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Seja bem-vindo(a) a lista de produtos!");
                Console.WriteLine("1 - Listagem\n2 - Adicionar\n3 - Remover\n4 - Sair");

                menu opcao = (menu)int.Parse(Console.ReadLine());

                switch (opcao)
                {

                    case menu.Listagem:
                        listagem();
                        break;

                    case menu.Adicionar:
                        adicionar();
                        break;

                    case menu.Remover:
                        remover();
                        break;

                    case menu.sair:
                        sair = true;
                        break;

                }
                Console.Clear();
            }

        }
        static void adicionar()
        {
            Console.Clear();
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente:");
            Console.WriteLine("..................................................................................");
            Console.Write("Qual o nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.Write("qual o e-mail do cliente: ");
            cliente.email = Console.ReadLine();
            Console.Write("Qual o cpf do cliente: ");
            cliente.cpf = Console.ReadLine();
            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("Cadastro realizado, presione ENTER para voltar ao menu!");
            Console.ReadLine();

        }
        static void listagem()
        {
            Console.Clear();
            Console.WriteLine("Exibindo lista de clientes");
            Console.WriteLine("..................................................................................");
            if (clientes.Count > 0)
            {
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("..................................................................................");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }
            Console.WriteLine("presione ENTER para voltar ao menu!");
            Console.ReadLine();
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();

        }
        static void Carregar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);

            try
            {

                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }
        static void remover()
        {
            listagem_remover();
            Console.WriteLine("..................................................................................");
            Console.Write("Digite o do cliente que vc deseja remover: ");

            var id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {

                clientes.RemoveAt(id);
                Salvar();

            }
            else
            {
                Console.WriteLine("ID digitado nao é valido, Tente novamente!");
                Console.ReadLine();
            }                     
        }
        static void listagem_remover()
        {
            Console.Clear();
            Console.WriteLine("Exibindo lista de clientes");
            Console.WriteLine("..................................................................................");
            if (clientes.Count > 0)
            {
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("..................................................................................");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }
        }
    }
}
