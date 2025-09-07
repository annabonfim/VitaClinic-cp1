# VitaClinic
# VitaClinic - Sistema de Gestão de Clínica

## Sobre o Projeto

O VitaClinic é um sistema completo de gestão de clínica médica desenvolvido em C# que permite cadastrar diferentes tipos de pacientes, médicos e agendar consultas com validações robustas e interface intuitiva. O projeto foi desenvolvido utilizando conceitos fundamentais de POO e boas práticas de desenvolvimento.

## Estrutura do Projeto

```
VitaClinic/
├── VitaClinic.Lib/           # Projeto de biblioteca (DLL)
│   ├── VitaClinic.Lib.csproj
│   ├── Pessoa.cs             # Classe base abstrata
│   ├── Paciente.cs           # Classe base para pacientes
│   ├── PacienteParticular.cs # Paciente particular
│   ├── PacienteConvenio.cs   # Paciente com convênio
│   ├── PacienteSUS.cs        # Paciente SUS
│   ├── Funcionario.cs        # Classe base para funcionários
│   ├── Medico.cs            # Médico especializado
│   ├── Consulta.cs          # Entidade consulta
│   └── Clinica.cs           # Gerenciador principal
└── VitaClinic.Console/       # Projeto de console
    ├── VitaClinic.Console.csproj
    └── Program.cs            # Interface do usuário com validações
```

## Requisitos Atendidos

### ✅ Estrutura Obrigatória
- **Projeto de biblioteca (.dll)** - VitaClinic.Lib
- **Projeto de console** - VitaClinic.Console que referencia a lib
- **Arquitetura modular** - Separação clara entre lógica e interface

### ✅ Conceitos de POO Implementados

#### Hierarquia de Classes
- **Pessoa** - Classe base abstrata
- **Paciente** - Classe abstrata que herda de Pessoa
- **Funcionario** - Classe abstrata que herda de Pessoa
- **Classes concretas** - PacienteParticular, PacienteConvenio, PacienteSUS, Medico

#### Classe Abstrata
- **Pessoa** - Classe base abstrata com método abstrato GetTipo()
- **Paciente** - Classe abstrata com implementações específicas para cada tipo
- **Funcionario** - Classe abstrata para funcionários da clínica

#### Polimorfismo com Override
- **GetTipo()** - Implementado diferentemente para cada tipo de pessoa
- **CalcularValor()** - Valores diferentes baseados no tipo de paciente
- **MostrarInfo()** - Exibição personalizada conforme o tipo

#### Encapsulamento
- **Propriedades públicas** - Nome, CPF para acesso controlado
- **Campos privados** - idade, proximoId para proteção dos dados
- **Métodos protegidos** - Para uso apenas pelas classes filhas

## Funcionalidades

### Tipos de Pessoas
- **Paciente Particular** - Cobrança direta com telefone para contato
- **Paciente Convênio** - Cobrança via convênio médico específico
- **Paciente SUS** - Atendimento gratuito com cartão SUS
- **Médico** - Funcionário com CRM, especialidade e salário

### Operações Disponíveis
1. ➕ Cadastrar pacientes (Particular, Convênio, SUS)
2. 👨‍⚕️ Cadastrar médicos com validações completas
3. 📅 Agendar consultas com validação de data
4. 📋 Listar pacientes cadastrados
5. 🏥 Listar médicos disponíveis
6. 📊 Listar consultas agendadas

### Validações Implementadas
- **Campos obrigatórios**: Nenhum campo pode ficar vazio
- **Números válidos**: Verificação de formato numérico
- **Valores positivos**: Idade e salário devem ser maiores que zero
- **Datas válidas**: Formato dd/MM/yyyy e não pode ser no passado
- **Seleções**: Validação de range para escolhas numéricas
- **Tratamento de erros**: Sistema resiliente com mensagens claras

## Dados de Teste

O sistema já vem com alguns dados pré-cadastrados para facilitar os testes:
- **João Silva** (Paciente Particular) - telefone: (11) 99999-9999
- **Maria Santos** (Paciente Convênio) - convênio: Unimed
- **Dr. Carlos Pereira** (Médico Cardiologista) - CRM: 12345-SP
- **Consulta agendada** para demonstração do sistema
