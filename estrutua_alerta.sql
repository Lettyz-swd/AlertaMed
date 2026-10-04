```sql
CREATE TABLE usuario (
    id_usuario      SERIAL PRIMARY KEY,
    nome            TEXT NOT NULL,
    email           TEXT NOT NULL UNIQUE,
    senha           TEXT NOT NULL,
    data_nascimento DATE,
    genero          TEXT,
    biografia       TEXT
);

CREATE TABLE instituicao (
    id_instituicao SERIAL PRIMARY KEY,
    nome           TEXT NOT NULL,
    tipo           TEXT NOT NULL,
    email          TEXT NOT NULL UNIQUE,
    senha          TEXT NOT NULL,
    data_criacao   TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),
    localizacao    TEXT,
    biografia      TEXT
);

CREATE TABLE membro_instituicao (
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

CREATE UNIQUE INDEX um_dono_por_instituicao
    ON membro_instituicao(id_instituicao)
    WHERE papel = 'dono';

CREATE TABLE solicitacao_entrada (
    id_solicitacao    SERIAL PRIMARY KEY,
    id_instituicao    INTEGER NOT NULL
        REFERENCES instituicao(id_instituicao),

    nome_solicitante  TEXT NOT NULL,
    email_solicitante TEXT NOT NULL,
    mensagem          TEXT,

    status TEXT NOT NULL DEFAULT 'pendente',

    data_solicitacao TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

    CONSTRAINT solicitacao_entrada_status_check
        CHECK (status IN ('pendente', 'aprovada', 'recusada'))
);

CREATE UNIQUE INDEX um_pedido_pendente
    ON solicitacao_entrada(id_instituicao, LOWER(email_solicitante))
    WHERE status = 'pendente';

CREATE TABLE paciente (
    id_paciente SERIAL PRIMARY KEY,

    nome TEXT NOT NULL,
    idade INTEGER,
    genero TEXT,
    peso NUMERIC(5,2),
    estado_civil TEXT,

    tem_filhos BOOLEAN NOT NULL DEFAULT FALSE,
    quantidade_filhos INTEGER,

    doencas_respiratorias BOOLEAN NOT NULL DEFAULT FALSE,
    quais_doencas_respiratorias TEXT,

    doencas_cardiovasculares BOOLEAN NOT NULL DEFAULT FALSE,
    quais_doencas_cardiovasculares TEXT,

    tem_alergias BOOLEAN NOT NULL DEFAULT FALSE,
    quais_alergias TEXT,

    informacoes_extras TEXT,
    anotacoes TEXT,

    id_usuario INTEGER NOT NULL
        REFERENCES usuario(id_usuario),

    data_cadastro TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

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

CREATE TABLE medicacao (
    id_medicamento   SERIAL PRIMARY KEY,
    nome_medicamento TEXT,
    dosagem          TEXT,
    intervalo_horas  INTEGER,
    duracao_dias     INTEGER,

    id_usuario INTEGER NOT NULL
        REFERENCES usuario(id_usuario)
);

CREATE TABLE alerta (
    id_alerta SERIAL PRIMARY KEY,

    data_hora_disparo TIMESTAMP WITHOUT TIME ZONE,
    status TEXT,

    id_medicamento INTEGER NOT NULL
        REFERENCES medicacao(id_medicamento)
);

CREATE TABLE prescricao (
    id_prescricao SERIAL PRIMARY KEY,

    nome_paciente TEXT NOT NULL,
    tecnico_responsavel TEXT NOT NULL,
    remedios TEXT NOT NULL,
    doses TEXT NOT NULL,
    horarios TEXT NOT NULL,

    data_cadastro TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),

    id_paciente INTEGER
        REFERENCES paciente(id_paciente),

    id_usuario INTEGER
        REFERENCES usuario(id_usuario),

    id_instituicao INTEGER
        REFERENCES instituicao(id_instituicao)
);

CREATE INDEX idx_prescricao_usuario
    ON prescricao(id_usuario);

CREATE INDEX idx_prescricao_instituicao
    ON prescricao(id_instituicao);
```
