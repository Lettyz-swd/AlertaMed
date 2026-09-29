CREATE TABLE usuario (
    id_usuario      SERIAL PRIMARY KEY,
    nome            text NOT NULL,
    email           text NOT NULL UNIQUE,
    senha           text NOT NULL,
    data_nascimento date,
    genero          text,
    biografia       text
);

CREATE TABLE instituicao (
    id_instituicao SERIAL PRIMARY KEY,
    nome           text NOT NULL,
    tipo           text NOT NULL,
    email          text NOT NULL UNIQUE,
    senha          text NOT NULL,
    data_criacao   timestamp without time zone NOT NULL DEFAULT now(),
    localizacao    text,
    biografia      text
);

CREATE TABLE membro_instituicao (
    id_instituicao integer NOT NULL REFERENCES instituicao (id_instituicao),
    id_usuario     integer NOT NULL REFERENCES usuario (id_usuario),
    papel          text NOT NULL DEFAULT 'membro',
    data_entrada   timestamp without time zone NOT NULL DEFAULT now(),
    PRIMARY KEY (id_instituicao, id_usuario),
    CONSTRAINT membro_instituicao_papel_check
        CHECK (papel IN ('dono', 'membro'))
);

CREATE UNIQUE INDEX um_dono_por_instituicao
    ON membro_instituicao (id_instituicao)
    WHERE papel = 'dono';

CREATE TABLE solicitacao_entrada (
    id_solicitacao    SERIAL PRIMARY KEY,
    id_instituicao    integer NOT NULL REFERENCES instituicao (id_instituicao),
    nome_solicitante  text NOT NULL,
    email_solicitante text NOT NULL,
    mensagem          text,
    status            text NOT NULL DEFAULT 'pendente',
    data_solicitacao  timestamp without time zone NOT NULL DEFAULT now(),
    CONSTRAINT solicitacao_entrada_status_check
        CHECK (status IN ('pendente', 'aprovada', 'recusada'))
);

CREATE UNIQUE INDEX um_pedido_pendente
    ON solicitacao_entrada (id_instituicao, lower(email_solicitante))
    WHERE status = 'pendente';

CREATE TABLE paciente (
    id_paciente                    SERIAL PRIMARY KEY,
    nome                           text NOT NULL,
    idade                          integer,
    genero                         text,
    peso                           numeric(5,2),
    estado_civil                   text,
    tem_filhos                     boolean NOT NULL DEFAULT false,
    quantidade_filhos              integer,
    doencas_respiratorias          boolean NOT NULL DEFAULT false,
    quais_doencas_respiratorias    text,
    doencas_cardiovasculares       boolean NOT NULL DEFAULT false,
    quais_doencas_cardiovasculares text,
    tem_alergias                   boolean NOT NULL DEFAULT false,
    quais_alergias                 text,
    informacoes_extras             text,
    anotacoes                      text,
    id_usuario                     integer NOT NULL REFERENCES usuario (id_usuario),
    data_cadastro                  timestamp without time zone NOT NULL DEFAULT now(),
    CONSTRAINT paciente_estado_civil_check
        CHECK (estado_civil IN ('solteiro', 'casado', 'viuvo', 'divorciado'))
);

CREATE TABLE medicacao (
    id_medicamento   SERIAL PRIMARY KEY,
    dosagem          text,
    intervalo_horas  integer,
    duracao_dias     integer,
    id_usuario       integer NOT NULL REFERENCES usuario (id_usuario),
    nome_medicamento text
);

CREATE TABLE alerta (
    id_alerta         SERIAL PRIMARY KEY,
    data_hora_disparo timestamp without time zone,
    status            text,
    id_medicamento    integer NOT NULL REFERENCES medicacao (id_medicamento)
);

CREATE TABLE prescricao (
    id_prescricao       SERIAL PRIMARY KEY,
    nome_paciente       text NOT NULL,
    tecnico_responsavel text NOT NULL,
    remedios            text NOT NULL,
    doses               text NOT NULL,
    horarios            text NOT NULL,
    data_cadastro       timestamp without time zone NOT NULL DEFAULT now(),
    id_paciente         integer REFERENCES paciente (id_paciente)
);
