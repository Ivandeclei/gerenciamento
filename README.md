# gerenciamento
gerenciamento de tarefas
## Fase 1: Execução da aplicação
**Ir no diretorio onde foi baixado a aplicação e rodar o comando para subir a aplicação nos containers**
    - docker-compose up --build
 ##   
**Apos os container estiverem rodando abrir o visual studio  e rodar no terminal PACK MANAGER CONSOLE -  OBS: Certifique-se de criar o banco de dados se o mesmo nao for criado**
-update-database

## Fase 2: Perguntas Para o PO

1. **Por que uma tarefa não pode mudar de prioridade?**
   - Quais são as razões por trás dessa restrição?
   - Existe alguma situação específica onde essa regra pode ser flexibilizada?
   - Quais são as implicações operacionais de permitir a mudança de prioridade?

2. **Por que um projeto tem limitação de quantidade de tarefas?**
   - Qual é a lógica por trás dessa limitação?
   - Existem critérios específicos para definir essa quantidade máxima?
   - Como essa limitação impacta a gestão e organização dos projetos?

## Fase 3: Melhorias

1. **Implementar autenticação da API**
   - Adicionar mecanismos de autenticação para garantir a segurança e a integridade dos dados.
   - Escolher o método de autenticação mais adequado (por exemplo, OAuth, JWT).
   - Documentar o processo de autenticação para desenvolvedores e usuários.

2. **Remover o vínculo da tabela de atualizações em relação a Tasks**
   - Avaliar a necessidade de manter um log detalhado de atualizações diretamente vinculado às tarefas.
   - Criar uma estratégia de arquivamento ou resumo de logs para evitar buscas extensas e melhorar a performance.
   - Implementar uma solução para buscar apenas as informações necessárias sem carregar um log excessivamente grande como por exemplo os comentarios.
