using System;
using VitaClinic.Lib;

namespace VitaClinic.Console
{
    class Program
    {
        static Clinica clinica = new Clinica();

        static void Main(string[] args)
        {
            CriarDadosTeste();

            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                string opcao = System.Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarPacienteParticular();
                        break;
                    case "2":
                        CadastrarPacienteConvenio();
                        break;
                    case "3":
                        CadastrarPacienteSUS();
                        break;
                    case "4":
                        CadastrarMedico();
                        break;
                    case "5":
                        AgendarConsulta();
                        break;
                    case "6":
                        clinica.ListarPacientes();
                        break;
                    case "7":
                        clinica.ListarMedicos();
                        break;
                    case "8":
                        clinica.ListarConsultas();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        System.Console.WriteLine("Opção inválida!");
                        break;
                }

                if (continuar)
                {
                    System.Console.WriteLine("\nPressione Enter para continuar...");
                    System.Console.ReadLine();
                }
            }

            System.Console.WriteLine("Obrigado por usar o VitaClinic!");
        }

        static void MostrarMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("           SISTEMA VITACLINIC");
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("1 - Cadastrar Paciente Particular");
            System.Console.WriteLine("2 - Cadastrar Paciente Convênio");
            System.Console.WriteLine("3 - Cadastrar Paciente SUS");
            System.Console.WriteLine("4 - Cadastrar Médico");
            System.Console.WriteLine("5 - Agendar Consulta");
            System.Console.WriteLine("6 - Listar Pacientes");
            System.Console.WriteLine("7 - Listar Médicos");
            System.Console.WriteLine("8 - Listar Consultas");
            System.Console.WriteLine("0 - Sair");
            System.Console.WriteLine("========================================");
            System.Console.Write("Digite sua opção: ");
        }

        // Método auxiliar para ler texto obrigatório
        static string LerTexto(string campo)
        {
            string texto;
            do
            {
                System.Console.Write($"{campo}: ");
                texto = System.Console.ReadLine();
                if (string.IsNullOrEmpty(texto))
                {
                    System.Console.WriteLine($"{campo} é obrigatório!");
                }
            } while (string.IsNullOrEmpty(texto));
            return texto;
        }

        // Método auxiliar para ler idade válida
        static int LerIdade()
        {
            int idade;
            do
            {
                System.Console.Write("Idade: ");
                if (!int.TryParse(System.Console.ReadLine(), out idade) || idade <= 0 || idade > 120)
                {
                    System.Console.WriteLine("Idade deve ser entre 1 e 120!");
                }
                else
                {
                    break;
                }
            } while (true);
            return idade;
        }

        static void CadastrarPacienteParticular()
        {
            System.Console.WriteLine("\n=== CADASTRO PACIENTE PARTICULAR ===");

            PacienteParticular paciente = new PacienteParticular();
            paciente.Nome = LerTexto("Nome");

            // CPF com validação básica
            do
            {
                paciente.CPF = LerTexto("CPF");
                if (!Pessoa.ValidarCPF(paciente.CPF))
                {
                    System.Console.WriteLine("CPF inválido!");
                }
                else
                {
                    break;
                }
            } while (true);

            paciente.telefone = LerTexto("Telefone");

            int idade = LerIdade();
            if (!paciente.SetIdade(idade))
            {
                System.Console.WriteLine("Erro ao definir idade!");
                return;
            }

            clinica.AdicionarPessoa(paciente);
        }

        static void CadastrarPacienteConvenio()
        {
            System.Console.WriteLine("\n=== CADASTRO PACIENTE CONVÊNIO ===");

            PacienteConvenio paciente = new PacienteConvenio();
            paciente.Nome = LerTexto("Nome");

            do
            {
                paciente.CPF = LerTexto("CPF");
                if (!Pessoa.ValidarCPF(paciente.CPF))
                {
                    System.Console.WriteLine("CPF inválido!");
                }
                else
                {
                    break;
                }
            } while (true);

            paciente.nomeConvenio = LerTexto("Nome do Convênio");

            int idade = LerIdade();
            paciente.SetIdade(idade);

            clinica.AdicionarPessoa(paciente);
        }

        static void CadastrarPacienteSUS()
        {
            System.Console.WriteLine("\n=== CADASTRO PACIENTE SUS ===");

            PacienteSUS paciente = new PacienteSUS();
            paciente.Nome = LerTexto("Nome");

            do
            {
                paciente.CPF = LerTexto("CPF");
                if (!Pessoa.ValidarCPF(paciente.CPF))
                {
                    System.Console.WriteLine("CPF inválido!");
                }
                else
                {
                    break;
                }
            } while (true);

            paciente.cartaoSUS = LerTexto("Cartão SUS");

            int idade = LerIdade();
            paciente.SetIdade(idade);

            clinica.AdicionarPessoa(paciente);
        }

        static void CadastrarMedico()
        {
            System.Console.WriteLine("\n=== CADASTRO MÉDICO ===");

            Medico medico = new Medico();
            medico.Nome = LerTexto("Nome");

            do
            {
                medico.CPF = LerTexto("CPF");
                if (!Pessoa.ValidarCPF(medico.CPF))
                {
                    System.Console.WriteLine("CPF inválido!");
                }
                else
                {
                    break;
                }
            } while (true);

            medico.crm = LerTexto("CRM");
            medico.especialidade = LerTexto("Especialidade");
            medico.matricula = LerTexto("Matrícula");

            // Salário simples
            decimal salario;
            do
            {
                System.Console.Write("Salário: ");
                if (!decimal.TryParse(System.Console.ReadLine(), out salario) || salario <= 0)
                {
                    System.Console.WriteLine("Salário deve ser maior que zero!");
                }
                else
                {
                    break;
                }
            } while (true);

            medico.salario = salario;

            clinica.AdicionarPessoa(medico);
        }

        static void AgendarConsulta()
        {
            System.Console.WriteLine("\n=== AGENDAR CONSULTA ===");

            var pacientes = clinica.ObterPacientes();
            var medicos = clinica.ObterMedicos();

            if (pacientes.Count == 0)
            {
                System.Console.WriteLine("Nenhum paciente cadastrado!");
                return;
            }

            if (medicos.Count == 0)
            {
                System.Console.WriteLine("Nenhum médico cadastrado!");
                return;
            }

            // Mostrar pacientes
            System.Console.WriteLine("\nPacientes:");
            for (int i = 0; i < pacientes.Count; i++)
            {
                System.Console.WriteLine($"{i + 1} - {pacientes[i].Nome} ({pacientes[i].GetTipo()})");
            }

            // Escolher paciente
            int numPaciente;
            do
            {
                System.Console.Write("Escolha o paciente (número): ");
                if (!int.TryParse(System.Console.ReadLine(), out numPaciente) ||
                    numPaciente < 1 || numPaciente > pacientes.Count)
                {
                    System.Console.WriteLine($"Digite um número de 1 a {pacientes.Count}!");
                }
                else
                {
                    break;
                }
            } while (true);

            // Mostrar médicos
            System.Console.WriteLine("\nMédicos:");
            for (int i = 0; i < medicos.Count; i++)
            {
                System.Console.WriteLine($"{i + 1} - Dr(a). {medicos[i].Nome} ({medicos[i].especialidade})");
            }

            // Escolher médico
            int numMedico;
            do
            {
                System.Console.Write("Escolha o médico (número): ");
                if (!int.TryParse(System.Console.ReadLine(), out numMedico) ||
                    numMedico < 1 || numMedico > medicos.Count)
                {
                    System.Console.WriteLine($"Digite um número de 1 a {medicos.Count}!");
                }
                else
                {
                    break;
                }
            } while (true);

            // Data
            string data;
            do
            {
                System.Console.Write("Data da consulta (dd/mm/aaaa): ");
                data = System.Console.ReadLine();
                if (!Consulta.ValidarData(data))
                {
                    System.Console.WriteLine("Data inválida! Use formato dd/mm/aaaa e a data deve ser hoje ou no futuro.");
                    System.Console.WriteLine("Exemplo: 20/01/2026");
                }
                else
                {
                    break;
                }
            } while (true);

            // Criar consulta
            var pacienteSelecionado = pacientes[numPaciente - 1];
            var medicoSelecionado = medicos[numMedico - 1];

            clinica.CriarConsulta(pacienteSelecionado, medicoSelecionado, data);
        }

        static void CriarDadosTeste()
        {
            PacienteParticular p1 = new PacienteParticular();
            p1.Nome = "João Silva";
            p1.CPF = "12345678901";
            p1.telefone = "(11) 99999-9999";
            p1.SetIdade(45);

            PacienteConvenio p2 = new PacienteConvenio();
            p2.Nome = "Maria Santos";
            p2.CPF = "98765432100";
            p2.nomeConvenio = "Unimed";
            p2.SetIdade(32);

            Medico m1 = new Medico();
            m1.Nome = "Carlos Pereira";
            m1.CPF = "45678912300";
            m1.crm = "12345-SP";
            m1.especialidade = "Cardiologista";
            m1.matricula = "MED001";
            m1.salario = 12000.00m;

            clinica.AdicionarPessoa(p1);
            clinica.AdicionarPessoa(p2);
            clinica.AdicionarPessoa(m1);
        }
    }
}