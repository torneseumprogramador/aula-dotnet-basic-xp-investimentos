using System;

namespace console_treinamento
{
    public class Program
    {
        public static readonly string SqlCNN = "Server=localhost;database=aula_xp;user=root;password=root";

        static void Main(string[] args)
        {
            new Cliente()
            {
                Nome = "Danilo",
                Telefone = "(11)99999-9999"
            }.Salvar();

            new Cliente()
            {
                Nome = "Silva",
                Telefone = "(43)99999-9999"
            }.Salvar();

            foreach (var cliente in Cliente.Todos())
            {
                cliente.Nome += " - Aluno XP Investimentos";
                cliente.Salvar();
            }

            //Cliente.DeletePorId(5);


            Console.WriteLine("Pessoas inseridas na base");
        }
    }
}
