-- ============================================================
-- AlertaMed: script completo do banco de dados
-- Seguro para rodar várias vezes.
--
-- Este script:
-- 1. Cria as tabelas caso ainda não existam;
-- 2. Adiciona colunas novas em bancos antigos;
-- 3. Cria índices caso ainda não existam;
-- 4. Mantém os dados existentes.
-- ============================================================


-- ============================================================
-- USUARIO
-- ============================================================

CREATE TABLE IF NOT EXISTS usuario (
    id_usuario      SERIAL PRIMARY KEY,
    nome            TEXT NOT NULL,
    email           TEXT NOT NULL UNIQUE,
    senha           TEXT NOT NULL,
    data_nascimento DATE,
    genero          TEXT,
    biografia       TEXT
);

ALTER TABLE usuario
    ADD COLUMN IF NOT EXISTS data_nascimento DATE;

ALTER TABLE usuario
    ADD COLUMN IF NOT EXISTS genero TEXT;

ALTER TABLE usuario
    ADD COLUMN IF NOT EXISTS biografia TEXT;


-- ============================================================
-- INSTITUICAO
-- ============================================================

CREATE TABLE IF NOT EXISTS instituicao (
    id_instituicao SERIAL PRIMARY KEY,
    nome           TEXT NOT NULL,
    tipo           TEXT NOT NULL,
    email          TEXT NOT NULL UNIQUE,
    senha          TEXT NOT NULL,
    data_criacao   TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),
    localizacao    TEXT,
    biografia      TEXT
);

ALTER TABLE instituicao
    ADD COLUMN IF NOT EXISTS localizacao TEXT;

ALTER TABLE instituicao
    ADD COLUMN IF NOT EXISTS biografia TEXT;

ALTER TABLE instituicao
    ADD COLUMN IF NOT EXISTS data_criacao
    TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW();


-- ============================================================
-- MEMBRO_INSTITUICAO
-- ============================================================

CREATE TABLE IF NOT EXISTS membro_instituicao (
    id_instituicao INTEGER NOT NULL
        REFERENCES instituicao(id_instituicao),

    id_usuario INTEGER NOT NULL
        REFERENCES usuario(id_usuario),

    papel TEXT NOT NULL DEFAULT 'membro',

    data_entrada TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

    PRIMARY KEY (id_instituicao, id_usuario),

    CONSTRAINT membro_instituicao_papel_check
        CHECK (papel IN ('dono', 'membro'))
);

ALTER TABLE membro_instituicao
    ADD COLUMN IF NOT EXISTS papel TEXT DEFAULT 'membro';

ALTER TABLE membro_instituicao
    ADD COLUMN IF NOT EXISTS data_entrada
    TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW();

CREATE UNIQUE INDEX IF NOT EXISTS um_dono_por_instituicao
    ON membro_instituicao(id_instituicao)
    WHERE papel = 'dono';


-- ============================================================
-- SOLICITACAO_ENTRADA
-- ============================================================

CREATE TABLE IF NOT EXISTS solicitacao_entrada (
    id_solicitacao    SERIAL PRIMARY KEY,
    id_instituicao    INTEGER NOT NULL
        REFERENCES instituicao(id_instituicao),

    nome_solicitante  TEXT NOT NULL,
    email_solicitante TEXT NOT NULL,
    mensagem          TEXT,

    status TEXT NOT NULL DEFAULT 'pendente',

    data_solicitacao
        TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

    CONSTRAINT solicitacao_entrada_status_check
        CHECK (status IN ('pendente', 'aprovada', 'recusada'))
);

ALTER TABLE solicitacao_entrada
    ADD COLUMN IF NOT EXISTS mensagem TEXT;

ALTER TABLE solicitacao_entrada
    ADD COLUMN IF NOT EXISTS status TEXT DEFAULT 'pendente';

ALTER TABLE solicitacao_entrada
    ADD COLUMN IF NOT EXISTS data_solicitacao
    TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW();

CREATE UNIQUE INDEX IF NOT EXISTS um_pedido_pendente
    ON solicitacao_entrada(
        id_instituicao,
        LOWER(email_solicitante)
    )
    WHERE status = 'pendente';


-- ============================================================
-- PACIENTE
-- ============================================================

CREATE TABLE IF NOT EXISTS paciente (
    id_paciente                    SERIAL PRIMARY KEY,
    nome                           TEXT NOT NULL,
    idade                          INTEGER,
    genero                         TEXT,
    peso                           NUMERIC(5,2),
    estado_civil                   TEXT,

    tem_filhos                     BOOLEAN NOT NULL DEFAULT FALSE,
    quantidade_filhos              INTEGER,

    doencas_respiratorias          BOOLEAN NOT NULL DEFAULT FALSE,
    quais_doencas_respiratorias    TEXT,

    doencas_cardiovasculares       BOOLEAN NOT NULL DEFAULT FALSE,
    quais_doencas_cardiovasculares TEXT,

    tem_alergias                   BOOLEAN NOT NULL DEFAULT FALSE,
    quais_alergias                 TEXT,

    informacoes_extras             TEXT,
    anotacoes                      TEXT,

    id_usuario                     INTEGER NOT NULL
        REFERENCES usuario(id_usuario),

    data_cadastro
        TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

    CONSTRAINT paciente_estado_civil_check
        CHECK (
            estado_civil IN (
                'solteiro',
                'casado',
                'viuvo',
                'divorciado'
            )
        )
);

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS genero TEXT;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS peso NUMERIC(5,2);

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS estado_civil TEXT;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS tem_filhos BOOLEAN DEFAULT FALSE;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS quantidade_filhos INTEGER;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS doencas_respiratorias BOOLEAN DEFAULT FALSE;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS quais_doencas_respiratorias TEXT;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS doencas_cardiovasculares BOOLEAN DEFAULT FALSE;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS quais_doencas_cardiovasculares TEXT;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS tem_alergias BOOLEAN DEFAULT FALSE;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS quais_alergias TEXT;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS informacoes_extras TEXT;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS anotacoes TEXT;

ALTER TABLE paciente
    ADD COLUMN IF NOT EXISTS data_cadastro
    TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW();


-- ============================================================
-- MEDICACAO
-- ============================================================

CREATE TABLE IF NOT EXISTS medicacao (
    id_medicamento   SERIAL PRIMARY KEY,
    nome_medicamento TEXT,
    dosagem          TEXT,
    intervalo_horas  INTEGER,
    duracao_dias     INTEGER,

    id_usuario INTEGER NOT NULL
        REFERENCES usuario(id_usuario)
);

ALTER TABLE medicacao
    ADD COLUMN IF NOT EXISTS nome_medicamento TEXT;

ALTER TABLE medicacao
    ADD COLUMN IF NOT EXISTS dosagem TEXT;

ALTER TABLE medicacao
    ADD COLUMN IF NOT EXISTS intervalo_horas INTEGER;

ALTER TABLE medicacao
    ADD COLUMN IF NOT EXISTS duracao_dias INTEGER;

ALTER TABLE medicacao
    ADD COLUMN IF NOT EXISTS id_usuario INTEGER;


-- ============================================================
-- ALERTA
-- ============================================================

CREATE TABLE IF NOT EXISTS alerta (
    id_alerta SERIAL PRIMARY KEY,

    data_hora_disparo TIMESTAMP WITHOUT TIME ZONE,
    status TEXT,

    id_medicamento INTEGER NOT NULL
        REFERENCES medicacao(id_medicamento)
);

ALTER TABLE alerta
    ADD COLUMN IF NOT EXISTS data_hora_disparo
    TIMESTAMP WITHOUT TIME ZONE;

ALTER TABLE alerta
    ADD COLUMN IF NOT EXISTS status TEXT;


-- ============================================================
-- PRESCRICAO
-- ============================================================

CREATE TABLE IF NOT EXISTS prescricao (
    id_prescricao SERIAL PRIMARY KEY,

    nome_paciente TEXT NOT NULL,
    tecnico_responsavel TEXT NOT NULL,
    remedios TEXT NOT NULL,
    doses TEXT NOT NULL,
    horarios TEXT NOT NULL,

    data_cadastro
        TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

    id_paciente INTEGER
        REFERENCES paciente(id_paciente),

    id_usuario INTEGER
        REFERENCES usuario(id_usuario),

    id_instituicao INTEGER
        REFERENCES instituicao(id_instituicao)
);


-- Colunas novas para bancos antigos

ALTER TABLE prescricao
    ADD COLUMN IF NOT EXISTS id_paciente INTEGER;

ALTER TABLE prescricao
    ADD COLUMN IF NOT EXISTS id_usuario INTEGER;

ALTER TABLE prescricao
    ADD COLUMN IF NOT EXISTS id_instituicao INTEGER;


-- ============================================================
-- CHAVES ESTRANGEIRAS DA PRESCRICAO
-- ============================================================

DO $$
BEGIN

    IF NOT EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'prescricao_id_paciente_fkey'
    ) THEN

        ALTER TABLE prescricao
            ADD CONSTRAINT prescricao_id_paciente_fkey
            FOREIGN KEY (id_paciente)
            REFERENCES paciente(id_paciente);

    END IF;


    IF NOT EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'fk_prescricao_usuario'
    ) THEN

        ALTER TABLE prescricao
            ADD CONSTRAINT fk_prescricao_usuario
            FOREIGN KEY (id_usuario)
            REFERENCES usuario(id_usuario);

    END IF;


    IF NOT EXISTS (
        SELECT 1
        FROM pg_constraint
        WHERE conname = 'fk_prescricao_instituicao'
    ) THEN

        ALTER TABLE prescricao
            ADD CONSTRAINT fk_prescricao_instituicao
            FOREIGN KEY (id_instituicao)
            REFERENCES instituicao(id_instituicao);

    END IF;

END $$;


-- ============================================================
-- INDICES DA PRESCRICAO
-- ============================================================

CREATE INDEX IF NOT EXISTS idx_prescricao_usuario
    ON prescricao(id_usuario);

CREATE INDEX IF NOT EXISTS idx_prescricao_instituicao
    ON prescricao(id_instituicao);