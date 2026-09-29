--
-- PostgreSQL database dump
--

\restrict o5rBvOIhilQ8kR5ePII4Uz7EdHPoRajaxgOKDQmAxWksO1ieZWMPM0JpTjWklal

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

-- Started on 2026-09-28 21:54:01

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 224 (class 1259 OID 16421)
-- Name: alerta; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.alerta (
    id_alerta integer NOT NULL,
    data_hora_disparo timestamp without time zone,
    status text,
    id_medicamento integer NOT NULL
);


--
-- TOC entry 223 (class 1259 OID 16420)
-- Name: alerta_id_alerta_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.alerta_id_alerta_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5045 (class 0 OID 0)
-- Dependencies: 223
-- Name: alerta_id_alerta_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.alerta_id_alerta_seq OWNED BY public.alerta.id_alerta;


--
-- TOC entry 226 (class 1259 OID 16518)
-- Name: instituicao; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.instituicao (
    id_instituicao integer NOT NULL,
    nome text NOT NULL,
    tipo text NOT NULL,
    email text NOT NULL,
    senha text NOT NULL,
    data_criacao timestamp without time zone DEFAULT now() NOT NULL,
    localizacao text,
    biografia text
);


--
-- TOC entry 225 (class 1259 OID 16517)
-- Name: instituicao_id_instituicao_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.instituicao_id_instituicao_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5046 (class 0 OID 0)
-- Dependencies: 225
-- Name: instituicao_id_instituicao_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.instituicao_id_instituicao_seq OWNED BY public.instituicao.id_instituicao;


--
-- TOC entry 222 (class 1259 OID 16405)
-- Name: medicacao; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.medicacao (
    id_medicamento integer NOT NULL,
    dosagem text,
    intervalo_horas integer,
    duracao_dias integer,
    id_usuario integer NOT NULL,
    nome_medicamento text
);


--
-- TOC entry 221 (class 1259 OID 16404)
-- Name: medicacao_id_medicamento_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.medicacao_id_medicamento_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5047 (class 0 OID 0)
-- Dependencies: 221
-- Name: medicacao_id_medicamento_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.medicacao_id_medicamento_seq OWNED BY public.medicacao.id_medicamento;


--
-- TOC entry 227 (class 1259 OID 16535)
-- Name: membro_instituicao; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.membro_instituicao (
    id_instituicao integer NOT NULL,
    id_usuario integer NOT NULL,
    papel text DEFAULT 'membro'::text NOT NULL,
    data_entrada timestamp without time zone DEFAULT now() NOT NULL,
    CONSTRAINT membro_instituicao_papel_check CHECK ((papel = ANY (ARRAY['dono'::text, 'membro'::text])))
);


--
-- TOC entry 231 (class 1259 OID 16585)
-- Name: paciente; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.paciente (
    id_paciente integer NOT NULL,
    nome text NOT NULL,
    idade integer,
    genero text,
    peso numeric(5,2),
    estado_civil text,
    tem_filhos boolean DEFAULT false NOT NULL,
    quantidade_filhos integer,
    doencas_respiratorias boolean DEFAULT false NOT NULL,
    quais_doencas_respiratorias text,
    doencas_cardiovasculares boolean DEFAULT false NOT NULL,
    quais_doencas_cardiovasculares text,
    tem_alergias boolean DEFAULT false NOT NULL,
    quais_alergias text,
    informacoes_extras text,
    anotacoes text,
    id_usuario integer NOT NULL,
    data_cadastro timestamp without time zone DEFAULT now() NOT NULL,
    CONSTRAINT paciente_estado_civil_check CHECK ((estado_civil = ANY (ARRAY['solteiro'::text, 'casado'::text, 'viuvo'::text, 'divorciado'::text])))
);


--
-- TOC entry 230 (class 1259 OID 16584)
-- Name: paciente_id_paciente_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.paciente_id_paciente_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5048 (class 0 OID 0)
-- Dependencies: 230
-- Name: paciente_id_paciente_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.paciente_id_paciente_seq OWNED BY public.paciente.id_paciente;


--
-- TOC entry 233 (class 1259 OID 16613)
-- Name: prescricao; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.prescricao (
    id_prescricao integer NOT NULL,
    nome_paciente text NOT NULL,
    tecnico_responsavel text NOT NULL,
    remedios text NOT NULL,
    doses text NOT NULL,
    horarios text NOT NULL,
    data_cadastro timestamp without time zone DEFAULT now() NOT NULL,
    id_paciente integer
);


--
-- TOC entry 232 (class 1259 OID 16612)
-- Name: prescricao_id_prescricao_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.prescricao_id_prescricao_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5049 (class 0 OID 0)
-- Dependencies: 232
-- Name: prescricao_id_prescricao_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.prescricao_id_prescricao_seq OWNED BY public.prescricao.id_prescricao;


--
-- TOC entry 229 (class 1259 OID 16561)
-- Name: solicitacao_entrada; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.solicitacao_entrada (
    id_solicitacao integer NOT NULL,
    id_instituicao integer NOT NULL,
    nome_solicitante text NOT NULL,
    email_solicitante text NOT NULL,
    mensagem text,
    status text DEFAULT 'pendente'::text NOT NULL,
    data_solicitacao timestamp without time zone DEFAULT now() NOT NULL,
    CONSTRAINT solicitacao_entrada_status_check CHECK ((status = ANY (ARRAY['pendente'::text, 'aprovada'::text, 'recusada'::text])))
);


--
-- TOC entry 228 (class 1259 OID 16560)
-- Name: solicitacao_entrada_id_solicitacao_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.solicitacao_entrada_id_solicitacao_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5050 (class 0 OID 0)
-- Dependencies: 228
-- Name: solicitacao_entrada_id_solicitacao_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.solicitacao_entrada_id_solicitacao_seq OWNED BY public.solicitacao_entrada.id_solicitacao;


--
-- TOC entry 220 (class 1259 OID 16390)
-- Name: usuario; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.usuario (
    id_usuario integer NOT NULL,
    nome text NOT NULL,
    email text NOT NULL,
    senha text NOT NULL,
    data_nascimento date,
    genero text,
    biografia text
);


--
-- TOC entry 219 (class 1259 OID 16389)
-- Name: usuario_id_usuario_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.usuario_id_usuario_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 5051 (class 0 OID 0)
-- Dependencies: 219
-- Name: usuario_id_usuario_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.usuario_id_usuario_seq OWNED BY public.usuario.id_usuario;


--
-- TOC entry 4845 (class 2604 OID 16424)
-- Name: alerta id_alerta; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.alerta ALTER COLUMN id_alerta SET DEFAULT nextval('public.alerta_id_alerta_seq'::regclass);


--
-- TOC entry 4846 (class 2604 OID 16521)
-- Name: instituicao id_instituicao; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.instituicao ALTER COLUMN id_instituicao SET DEFAULT nextval('public.instituicao_id_instituicao_seq'::regclass);


--
-- TOC entry 4844 (class 2604 OID 16408)
-- Name: medicacao id_medicamento; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.medicacao ALTER COLUMN id_medicamento SET DEFAULT nextval('public.medicacao_id_medicamento_seq'::regclass);


--
-- TOC entry 4853 (class 2604 OID 16588)
-- Name: paciente id_paciente; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.paciente ALTER COLUMN id_paciente SET DEFAULT nextval('public.paciente_id_paciente_seq'::regclass);


--
-- TOC entry 4859 (class 2604 OID 16616)
-- Name: prescricao id_prescricao; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.prescricao ALTER COLUMN id_prescricao SET DEFAULT nextval('public.prescricao_id_prescricao_seq'::regclass);


--
-- TOC entry 4850 (class 2604 OID 16564)
-- Name: solicitacao_entrada id_solicitacao; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.solicitacao_entrada ALTER COLUMN id_solicitacao SET DEFAULT nextval('public.solicitacao_entrada_id_solicitacao_seq'::regclass);


--
-- TOC entry 4843 (class 2604 OID 16393)
-- Name: usuario id_usuario; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.usuario ALTER COLUMN id_usuario SET DEFAULT nextval('public.usuario_id_usuario_seq'::regclass);


--
-- TOC entry 4871 (class 2606 OID 16430)
-- Name: alerta alerta_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.alerta
    ADD CONSTRAINT alerta_pkey PRIMARY KEY (id_alerta);


--
-- TOC entry 4873 (class 2606 OID 16534)
-- Name: instituicao instituicao_email_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.instituicao
    ADD CONSTRAINT instituicao_email_key UNIQUE (email);


--
-- TOC entry 4875 (class 2606 OID 16532)
-- Name: instituicao instituicao_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.instituicao
    ADD CONSTRAINT instituicao_pkey PRIMARY KEY (id_instituicao);


--
-- TOC entry 4869 (class 2606 OID 16414)
-- Name: medicacao medicacao_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.medicacao
    ADD CONSTRAINT medicacao_pkey PRIMARY KEY (id_medicamento);


--
-- TOC entry 4877 (class 2606 OID 16548)
-- Name: membro_instituicao membro_instituicao_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.membro_instituicao
    ADD CONSTRAINT membro_instituicao_pkey PRIMARY KEY (id_instituicao, id_usuario);


--
-- TOC entry 4883 (class 2606 OID 16606)
-- Name: paciente paciente_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.paciente
    ADD CONSTRAINT paciente_pkey PRIMARY KEY (id_paciente);


--
-- TOC entry 4885 (class 2606 OID 16628)
-- Name: prescricao prescricao_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.prescricao
    ADD CONSTRAINT prescricao_pkey PRIMARY KEY (id_prescricao);


--
-- TOC entry 4880 (class 2606 OID 16577)
-- Name: solicitacao_entrada solicitacao_entrada_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.solicitacao_entrada
    ADD CONSTRAINT solicitacao_entrada_pkey PRIMARY KEY (id_solicitacao);


--
-- TOC entry 4865 (class 2606 OID 16403)
-- Name: usuario usuario_email_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_email_key UNIQUE (email);


--
-- TOC entry 4867 (class 2606 OID 16401)
-- Name: usuario usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id_usuario);


--
-- TOC entry 4878 (class 1259 OID 16559)
-- Name: um_dono_por_instituicao; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX um_dono_por_instituicao ON public.membro_instituicao USING btree (id_instituicao) WHERE (papel = 'dono'::text);


--
-- TOC entry 4881 (class 1259 OID 16583)
-- Name: um_pedido_pendente; Type: INDEX; Schema: public; Owner: -
--

CREATE UNIQUE INDEX um_pedido_pendente ON public.solicitacao_entrada USING btree (id_instituicao, lower(email_solicitante)) WHERE (status = 'pendente'::text);


--
-- TOC entry 4887 (class 2606 OID 16431)
-- Name: alerta alerta_id_medicamento_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.alerta
    ADD CONSTRAINT alerta_id_medicamento_fkey FOREIGN KEY (id_medicamento) REFERENCES public.medicacao(id_medicamento);


--
-- TOC entry 4886 (class 2606 OID 16415)
-- Name: medicacao medicacao_id_usuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.medicacao
    ADD CONSTRAINT medicacao_id_usuario_fkey FOREIGN KEY (id_usuario) REFERENCES public.usuario(id_usuario);


--
-- TOC entry 4888 (class 2606 OID 16549)
-- Name: membro_instituicao membro_instituicao_id_instituicao_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.membro_instituicao
    ADD CONSTRAINT membro_instituicao_id_instituicao_fkey FOREIGN KEY (id_instituicao) REFERENCES public.instituicao(id_instituicao);


--
-- TOC entry 4889 (class 2606 OID 16554)
-- Name: membro_instituicao membro_instituicao_id_usuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.membro_instituicao
    ADD CONSTRAINT membro_instituicao_id_usuario_fkey FOREIGN KEY (id_usuario) REFERENCES public.usuario(id_usuario);


--
-- TOC entry 4891 (class 2606 OID 16607)
-- Name: paciente paciente_id_usuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.paciente
    ADD CONSTRAINT paciente_id_usuario_fkey FOREIGN KEY (id_usuario) REFERENCES public.usuario(id_usuario);


--
-- TOC entry 4892 (class 2606 OID 16672)
-- Name: prescricao prescricao_id_paciente_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.prescricao
    ADD CONSTRAINT prescricao_id_paciente_fkey FOREIGN KEY (id_paciente) REFERENCES public.paciente(id_paciente);


--
-- TOC entry 4890 (class 2606 OID 16578)
-- Name: solicitacao_entrada solicitacao_entrada_id_instituicao_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.solicitacao_entrada
    ADD CONSTRAINT solicitacao_entrada_id_instituicao_fkey FOREIGN KEY (id_instituicao) REFERENCES public.instituicao(id_instituicao);


-- Completed on 2026-09-28 21:54:01

--
-- PostgreSQL database dump complete
--

\unrestrict o5rBvOIhilQ8kR5ePII4Uz7EdHPoRajaxgOKDQmAxWksO1ieZWMPM0JpTjWklal

