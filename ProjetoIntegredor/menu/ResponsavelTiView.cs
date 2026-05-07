using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class ResponsavelTiView
    {
        public static string CadSoftwareInterface(Usuario usuarioLogado, SoftwareService softwareService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.RT)
            {
                while (true)
                {
                    Console.WriteLine("\n====== CADASTRAR SOFTWARE ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o nome do software: ");
                    string? nomeSoftware = Console.ReadLine();
                    if (nomeSoftware == "0") return "Cadastro cancelado.";

                    Console.Write("Informe a versão do software: ");
                    string? versao = Console.ReadLine();
                    if (versao == "0") return "Cadastro cancelado.";

                    try
                    {
                        var softwareCadastrado = softwareService.CadastrarSoftware(usuarioLogado, nomeSoftware!, versao!);

                        return $"Software {softwareCadastrado.NomeSoftware} cadastrado com sucesso!";
                    }
                    catch (ArgumentException ex)
                    {
                        return $"Erro ao cadastrar usuário: {ex.Message}";
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        return $"Erro: {ex.Message}";
                    }
                }
            }
            else
            {
                return "Erro: Apenas responsáveis pelo TI podem executar essa ação!";
            }
        }
    }
}