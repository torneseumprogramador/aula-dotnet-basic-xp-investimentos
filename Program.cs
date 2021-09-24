using System;

namespace console_treinamento
{
    public class Program
    {
        public static readonly string SqlCNN = "Server=localhost;Database=aula_xp;Uid=sa;Pwd=!1#2a3d4c5g6v";

        static void Main(string[] args)
        {
            /*
            foreach(var cliente in Cliente.Todos())
            {
                cliente.Nome += " - Aluno XP Investimentos";
                cliente.Salvar();
            }
            */

            Cliente.DeletePorId(5);


            Console.WriteLine("Pessoas inseridas na base");
        }
    }
}
