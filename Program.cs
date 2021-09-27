using System;

namespace console_treinamento
{
    public class Program
    {
        public static readonly string SqlCNN = "Server=localhost;database=aula_xp;user=root;password=root;SslMode=none";

        static void Main(string[] args)
        {
            var repoCliente = new PessoaRepositorio(typeof(Cliente));
            var repoFornecedor = new PessoaRepositorio(typeof(Fornecedor));
            

            Console.WriteLine("------------[Clientes]-------------");
            foreach (var cliente in repoCliente.Todos())
            {
                Console.WriteLine($"Id {cliente.Id}");
                Console.WriteLine($"Nome {cliente.Nome}");
                Console.WriteLine($"Telefone {cliente.Telefone}");
                Console.WriteLine("----------------------------");
            }

            Console.WriteLine("\n");

            Console.WriteLine("------------[Fornecedores]----------------");
            foreach (var cliente in repoFornecedor.Todos())
            {
                Console.WriteLine($"Id {cliente.Id}");
                Console.WriteLine($"Nome {cliente.Nome}");
                Console.WriteLine($"Telefone {cliente.Telefone}");
                Console.WriteLine("----------------------------");
            }




            /*repoCliente.Salvar(new Cliente()
            {
                Nome = "Lana",
                Telefone = "(11)99999-9999"
            });

            repoCliente.Salvar(new Cliente()
            {
                Nome = "Denilson",
                Telefone = "(11)99999-9999"
            });*/


            //Cliente.DeletePorId(5);


            Console.WriteLine("Pessoas inseridas na base");
        }
    }
}
