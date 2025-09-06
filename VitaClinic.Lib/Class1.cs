using System;
using System.Collections.Generic;
using System.Linq;

namespace VitaClinic.Lib
{
    // CLASSE ABSTRATA - obrigatório
    public abstract class Pessoa
    {
        // 1. public 
        public string Nome = string.Empty;
        public string CPF = string.Empty;

        // 2. private 
        private int idade;

        // 3. protected 
        protected string codigo = string.Empty;

        // 4. internal 
        internal string observacoes = string.Empty;

        // 5. protected internal 
        protected internal void MostrarInfo()
        {
            Console.WriteLine($"Nome: {Nome}, CPF: {CPF}");
        }

        // 6. private protected 
        private protected void ProcessarDados()
        {
            codigo = "PES" + DateTime.Now.Year;
        }

        // 7. Como alternativa ao 'file', usamos private static
        private static bool ValidarCPFInterno(string cpf)
        {
            return !string.IsNullOrEmpty(cpf);
        }

        // MÉTODO ABSTRATO - deve ser implementado
        public abstract decimal CalcularValor();

        // MÉTODO VIRTUAL - pode ser sobrescrito (POLIMORFISMO)
        public virtual string GetTipo()
        {
            return "Pessoa Genérica";
        }

        public bool SetIdade(int novaIdade)
        {
            if (novaIdade > 0 && novaIdade <= 120)
            {
                idade = novaIdade;
                return true;
            }
            return false;
        }

        public int GetIdade()
        {
            return idade;
        }

        // Validação simples de CPF
        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;
            cpf = cpf.Replace(".", "").Replace("-", "");
            return cpf.Length == 11 && cpf.All(char.IsDigit) && ValidarCPFInterno(cpf);
        }
    }

    // PACIENTES - POLIMORFISMO com override
    public class PacienteParticular : Pessoa
    {
        public string telefone = string.Empty;

        public override decimal CalcularValor()
        {
            return 150.00m;
        }

        public override string GetTipo()
        {
            return "Paciente Particular";
        }
    }

    public class PacienteConvenio : Pessoa
    {
        public string nomeConvenio = string.Empty;

        public override decimal CalcularValor()
        {
            return 25.00m;
        }

        public override string GetTipo()
        {
            return "Paciente Convênio";
        }
    }

    public class PacienteSUS : Pessoa
    {
        public string cartaoSUS = string.Empty;

        public override decimal CalcularValor()
        {
            return 0.00m;
        }

        public override string GetTipo()
        {
            return "Paciente SUS";
        }
    }

    // FUNCIONÁRIOS - classe abstrata
    public abstract class Funcionario : Pessoa
    {
        public string matricula = string.Empty;
        public decimal salario;

        public abstract void Trabalhar();
    }

    public class Medico : Funcionario
    {
        public string especialidade = string.Empty;
        public string crm = string.Empty;

        public override decimal CalcularValor()
        {
            return 0.00m;
        }

        public override string GetTipo()
        {
            return "Médico";
        }

        public override void Trabalhar()
        {
            Console.WriteLine($"Dr(a). {Nome} está atendendo pacientes - {especialidade}");
        }
    }

    public class Enfermeiro : Funcionario
    {
        public string coren = string.Empty;

        public override decimal CalcularValor()
        {
            return 0.00m;
        }

        public override string GetTipo()
        {
            return "Enfermeiro(a)";
        }

        public override void Trabalhar()
        {
            Console.WriteLine($"{Nome} está cuidando dos pacientes");
        }
    }

    public class Recepcionista : Funcionario
    {
        public override decimal CalcularValor()
        {
            return 0.00m;
        }

        public override string GetTipo()
        {
            return "Recepcionista";
        }

        public override void Trabalhar()
        {
            Console.WriteLine($"{Nome} está atendendo na recepção");
        }
    }

    // CONSULTA
    public class Consulta
    {
        public int id;
        public Pessoa paciente = null!;
        public Medico medico = null!;
        public string data = string.Empty;
        public decimal valor;

        public void MostrarConsulta()
        {
            Console.WriteLine($"\n--- CONSULTA {id} ---");
            Console.WriteLine($"Paciente: {paciente.Nome} ({paciente.GetTipo()})");
            Console.WriteLine($"Médico: {medico.Nome}");
            Console.WriteLine($"Data: {data}");
            Console.WriteLine($"Valor: R$ {valor:F2}");
        }

        public static bool ValidarData(string dataStr)
        {
            if (DateTime.TryParse(dataStr, out DateTime resultado))
            {
                return resultado.Date >= DateTime.Now.Date;
            }
            return false;
        }
    }

    // CLÍNICA
    public class Clinica
    {
        public List<Pessoa> pessoas = new List<Pessoa>();
        public List<Consulta> consultas = new List<Consulta>();
        private int proximoId = 1;

        public bool AdicionarPessoa(Pessoa pessoa)
        {
            if (pessoa == null || string.IsNullOrEmpty(pessoa.Nome))
                return false;

            pessoas.Add(pessoa);
            Console.WriteLine($"{pessoa.GetTipo()} {pessoa.Nome} foi cadastrado!");
            return true;
        }

        public bool CriarConsulta(Pessoa paciente, Medico medico, string data)
        {
            if (paciente == null || medico == null)
            {
                Console.WriteLine("Erro: Paciente ou médico inválido!");
                return false;
            }

            if (!Consulta.ValidarData(data))
            {
                Console.WriteLine("Erro: Data deve ser hoje ou no futuro!");
                return false;
            }

            Consulta consulta = new Consulta();
            consulta.id = proximoId++;
            consulta.paciente = paciente;
            consulta.medico = medico;
            consulta.data = data;
            consulta.valor = paciente.CalcularValor(); // POLIMORFISMO!

            consultas.Add(consulta);
            Console.WriteLine($"Consulta agendada! Valor: R$ {consulta.valor:F2}");
            return true;
        }

        public void ListarPacientes()
        {
            Console.WriteLine("\n=== PACIENTES ===");
            var pacientes = pessoas.Where(p => !(p is Funcionario)).ToList();

            if (pacientes.Count == 0)
            {
                Console.WriteLine("Nenhum paciente cadastrado.");
                return;
            }

            foreach (Pessoa pessoa in pacientes)
            {
                Console.WriteLine($"- {pessoa.Nome} ({pessoa.GetTipo()}) - Taxa: R$ {pessoa.CalcularValor():F2}");
            }
        }

        public void ListarMedicos()
        {
            Console.WriteLine("\n=== MÉDICOS ===");
            var medicos = pessoas.Where(p => p is Medico).Cast<Medico>().ToList();

            if (medicos.Count == 0)
            {
                Console.WriteLine("Nenhum médico cadastrado.");
                return;
            }

            foreach (Medico med in medicos)
            {
                Console.WriteLine($"- Dr(a). {med.Nome} - {med.especialidade}");
            }
        }

        public void ListarConsultas()
        {
            Console.WriteLine("\n=== CONSULTAS AGENDADAS ===");
            if (consultas.Count == 0)
            {
                Console.WriteLine("Nenhuma consulta agendada.");
                return;
            }

            foreach (Consulta consulta in consultas)
            {
                consulta.MostrarConsulta();
            }
        }

        public List<Pessoa> ObterPacientes()
        {
            return pessoas.Where(p => !(p is Funcionario)).ToList();
        }

        public List<Medico> ObterMedicos()
        {
            return pessoas.Where(p => p is Medico).Cast<Medico>().ToList();
        }
    }
}

