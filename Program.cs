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
            


            repoCliente.Salvar(new Cliente()
            {
                Nome = "Daniela 2",
                Telefone = "(11)99999-9999",
                CPF = "404.324.670-68"
            });

            repoCliente.Salvar(new Fornecedor()
            {
                Nome = "Xp investimentos 2",
                Telefone = "(11)99999-9999",
                CNPJ = "23.332.060/0001-07"
            });



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

            //Cliente.DeletePorId(5);


            Console.WriteLine("Pessoas inseridas na base");
        }
    }
}
