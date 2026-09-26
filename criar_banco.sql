-- AlertaMed: script COMPLETO do banco de dados
--
-- Pode ser rodado em qualquer computador, mesmo se as tabelas
-- antigas ja existirem (usa IF NOT EXISTS): nao apaga nem duplica nada.
--
-- Como usar (pgAdmin 4):
--   1. Crie um banco vazio chamado "alertamed" (se ainda nao existir)
--   2. Botao direito no banco "alertamed" > Query Tool
--   3. Cole este script (ou abra o arquivo) e aperte F5

-- ============================================================
-- USUARIO (cadastro e login de pessoas)
-- ============================================================
CREATE TABLE IF NOT EXISTS public.usuario (
    id_usuario serial PRIMARY KEY,
    nome       text NOT NULL,
    email      text NOT NULL UNIQUE,
    senha      text NOT NULL            -- hash (classe Senha), nunca texto puro
);

-- novo: o dono da instituicao precisa ter 18 anos ou mais
-- (a validacao da idade fica no C#; coluna vazia para usuarios comuns)
ALTER TABLE public.usuario ADD COLUMN IF NOT EXISTS data_nascimento date;

-- ============================================================
-- MEDICACAO e ALERTA (parte do colega)
-- ============================================================
CREATE TABLE IF NOT EXISTS public.medicacao (
    id_medicamento   serial PRIMARY KEY,
    nome_medicamento text,
    dosagem          text,
    intervalo_horas  integer,
    duracao_dias     integer,
    id_usuario       integer NOT NULL REFERENCES public.usuario (id_usuario)
);

ALTER TABLE public.medicacao ADD COLUMN IF NOT EXISTS nome_medicamento text;

CREATE TABLE IF NOT EXISTS public.alerta (
    id_alerta         serial PRIMARY KEY,
    data_hora_disparo timestamp,
    status            text,
    id_medicamento    integer NOT NULL REFERENCES public.medicacao (id_medicamento)
);

-- ============================================================
-- INSTITUICAO (Form2: criar conta / Form9: login da instituicao)
-- ============================================================
CREATE TABLE IF NOT EXISTS public.instituicao (
    id_instituicao serial PRIMARY KEY,
    nome           text NOT NULL,
    tipo           text NOT NULL,       -- valor escolhido no ComboBox do Form2
    email          text NOT NULL UNIQUE,
    senha          text NOT NULL,       -- hash (classe Senha), nunca texto puro
    data_criacao   timestamp NOT NULL DEFAULT now()
);

-- ============================================================
-- MEMBRO_INSTITUICAO (quem faz parte de qual instituicao)
-- O dono (Form4) entra aqui com papel = 'dono'.
-- Quem tem o pedido aprovado entra com papel = 'membro'.
-- ============================================================
CREATE TABLE IF NOT EXISTS public.membro_instituicao (
    id_instituicao integer NOT NULL REFERENCES public.instituicao (id_instituicao),
    id_usuario     integer NOT NULL REFERENCES public.usuario (id_usuario),
    papel          text NOT NULL DEFAULT 'membro' CHECK (papel IN ('dono', 'membro')),
    data_entrada   timestamp NOT NULL DEFAULT now(),
    PRIMARY KEY (id_instituicao, id_usuario)
);

-- garante no maximo UM dono por instituicao
CREATE UNIQUE INDEX IF NOT EXISTS um_dono_por_instituicao
    ON public.membro_instituicao (id_instituicao)
    WHERE papel = 'dono';

-- ============================================================
-- SOLICITACAO_ENTRADA (Form8: pedido para entrar em uma instituicao)
-- ============================================================
CREATE TABLE IF NOT EXISTS public.solicitacao_entrada (
    id_solicitacao    serial PRIMARY KEY,
    id_instituicao    integer NOT NULL REFERENCES public.instituicao (id_instituicao),
    nome_solicitante  text NOT NULL,
    email_solicitante text NOT NULL,
    mensagem          text,
    status            text NOT NULL DEFAULT 'pendente'
                      CHECK (status IN ('pendente', 'aprovada', 'recusada')),
    data_solicitacao  timestamp NOT NULL DEFAULT now()
);

-- impede a mesma pessoa de mandar dois pedidos pendentes para a mesma instituicao
CREATE UNIQUE INDEX IF NOT EXISTS um_pedido_pendente
    ON public.solicitacao_entrada (id_instituicao, lower(email_solicitante))
    WHERE status = 'pendente';
