using System;
using System.Collections.Generic;

namespace VitaClinic.Lib
{
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

        // 7. private static
        private static bool ValidarCPFInterno(string cpf)
        {
            return !string.IsNullOrEmpty(cpf);
        }

        public abstract decimal CalcularValor();

        public virtual string GetTipo()
        {
            return "Pessoa";
        }

        public void SetIdade(int novaIdade)
        {
            if (novaIdade > 0)
                idade = novaIdade;
        }

        public int GetIdade()
        {
            return idade;
        }

        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ", "");

            if (cpf.Length != 11) return false;

            for (int i = 0; i < cpf.Length; i++)
            {
                if (!char.IsDigit(cpf[i]))
                    return false;
            }

            return ValidarCPFInterno(cpf);
        }
    }

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

        public static bool ValidarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone)) return false;

            string apenasNumeros = "";
            for (int i = 0; i < telefone.Length; i++)
            {
                if (char.IsDigit(telefone[i]))
                    apenasNumeros += telefone[i];
            }

            return apenasNumeros.Length >= 10 && apenasNumeros.Length <= 11;
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

    public abstract class Funcionario : Pessoa
    {
        public string matricula = string.Empty;

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
            Console.WriteLine($"Dr(a). {Nome} está atendendo - {especialidade}");
        }
    }

    public class Consulta
    {
        public int id;
        public Pessoa paciente = null!;
        public Medico medico = null!;
        public string data = string.Empty;
        public decimal valor;

        public void MostrarConsulta()
        {
            Console.WriteLine($"\nConsulta {id}: {paciente.Nome} com Dr(a). {medico.Nome} em {data} - R$ {valor:F2}");
        }

        public static bool ValidarData(string dataStr)
        {
            if (DateTime.TryParseExact(dataStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime resultado))
            {
                return resultado.Date >= DateTime.Now.Date;
            }
            return false;
        }
    }

    public class Clinica
    {
        public List<Pessoa> pessoas = new List<Pessoa>();
        public List<Consulta> consultas = new List<Consulta>();
        private int proximoId = 1;

        public void AdicionarPessoa(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
            Console.WriteLine($"{pessoa.GetTipo()} {pessoa.Nome} cadastrado!");
        }

        public void CriarConsulta(Pessoa paciente, Medico medico, string data)
        {
            Consulta consulta = new Consulta();
            consulta.id = proximoId++;
            consulta.paciente = paciente;
            consulta.medico = medico;
            consulta.data = data;
            consulta.valor = paciente.CalcularValor();

            consultas.Add(consulta);
            Console.WriteLine($"Consulta agendada! Valor: R$ {consulta.valor:F2}");
        }

        public void ListarPacientes()
        {
            Console.WriteLine("\n=== PACIENTES ===");
            foreach (Pessoa pessoa in pessoas)
            {
                if (!(pessoa is Funcionario))
                {
                    Console.WriteLine($"- {pessoa.Nome} ({pessoa.GetTipo()}) - R$ {pessoa.CalcularValor():F2}");
                }
            }
        }

        public void ListarMedicos()
        {
            Console.WriteLine("\n=== MÉDICOS ===");
            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa is Medico)
                {
                    Medico med = (Medico)pessoa;
                    Console.WriteLine($"- Dr(a). {med.Nome} - {med.especialidade}");
                    med.Trabalhar();
                }
            }
        }

        public void ListarConsultas()
        {
            Console.WriteLine("\n=== CONSULTAS ===");
            foreach (Consulta consulta in consultas)
            {
                consulta.MostrarConsulta();
            }
        }

        public List<Pessoa> ObterPacientes()
        {
            List<Pessoa> pacientes = new List<Pessoa>();
            foreach (Pessoa pessoa in pessoas)
            {
                if (!(pessoa is Funcionario))
                    pacientes.Add(pessoa);
            }
            return pacientes;
        }

        public List<Medico> ObterMedicos()
        {
            List<Medico> medicos = new List<Medico>();
            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa is Medico)
                    medicos.Add((Medico)pessoa);
            }
            return medicos;
        }
    }
}