using System;

namespace console_treinamento
{
    public class ClienteRepositorio
    {
        public void Salvar(IFisica fisica)
        {

            var cliente = (Cliente)fisica;
            //Cursor com o banco
            // salvar lá no SQL
            Console.WriteLine("Estou salvando no banco o " + fisica.Nome);
        }
    }
}
