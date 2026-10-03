--
-- PostgreSQL database dump
--

\restrict Ym0qRZP6mcH5ruVOx4WCDBsrXsR9QGQogWMqt89NbmkbdAl4OLfVNRgWzxGtXtd

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

-- Started on 2026-09-28 19:52:08

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
-- Name: alerta; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.alerta (
    id_alerta integer NOT NULL,
    data_hora_disparo timestamp without time zone,
    status text,
    id_medicamento integer NOT NULL
);


ALTER TABLE public.alerta OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16420)
-- Name: alerta_id_alerta_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.alerta_id_alerta_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.alerta_id_alerta_seq OWNER TO postgres;

--
-- TOC entry 5060 (class 0 OID 0)
-- Dependencies: 223
-- Name: alerta_id_alerta_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.alerta_id_alerta_seq OWNED BY public.alerta.id_alerta;


--
-- TOC entry 226 (class 1259 OID 16518)
-- Name: instituicao; Type: TABLE; Schema: public; Owner: postgres
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


ALTER TABLE public.instituicao OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 16517)
-- Name: instituicao_id_instituicao_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.instituicao_id_instituicao_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.instituicao_id_instituicao_seq OWNER TO postgres;

--
-- TOC entry 5061 (class 0 OID 0)
-- Dependencies: 225
-- Name: instituicao_id_instituicao_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.instituicao_id_instituicao_seq OWNED BY public.instituicao.id_instituicao;


--
-- TOC entry 222 (class 1259 OID 16405)
-- Name: medicacao; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.medicacao (
    id_medicamento integer NOT NULL,
    dosagem text,
    intervalo_horas integer,
    duracao_dias integer,
    id_usuario integer NOT NULL,
    nome_medicamento text
);


ALTER TABLE public.medicacao OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16404)
-- Name: medicacao_id_medicamento_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.medicacao_id_medicamento_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.medicacao_id_medicamento_seq OWNER TO postgres;

--
-- TOC entry 5062 (class 0 OID 0)
-- Dependencies: 221
-- Name: medicacao_id_medicamento_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.medicacao_id_medicamento_seq OWNED BY public.medicacao.id_medicamento;


--
-- TOC entry 227 (class 1259 OID 16535)
-- Name: membro_instituicao; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.membro_instituicao (
    id_instituicao integer NOT NULL,
    id_usuario integer NOT NULL,
    papel text DEFAULT 'membro'::text NOT NULL,
    data_entrada timestamp without time zone DEFAULT now() NOT NULL,
    CONSTRAINT membro_instituicao_papel_check CHECK ((papel = ANY (ARRAY['dono'::text, 'membro'::text])))
);


ALTER TABLE public.membro_instituicao OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 16585)
-- Name: paciente; Type: TABLE; Schema: public; Owner: postgres
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


ALTER TABLE public.paciente OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 16584)
-- Name: paciente_id_paciente_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.paciente_id_paciente_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.paciente_id_paciente_seq OWNER TO postgres;

--
-- TOC entry 5063 (class 0 OID 0)
-- Dependencies: 230
-- Name: paciente_id_paciente_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.paciente_id_paciente_seq OWNED BY public.paciente.id_paciente;


--
-- TOC entry 233 (class 1259 OID 16613)
-- Name: prescricao; Type: TABLE; Schema: public; Owner: postgres
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


ALTER TABLE public.prescricao OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 16612)
-- Name: prescricao_id_prescricao_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.prescricao_id_prescricao_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.prescricao_id_prescricao_seq OWNER TO postgres;

--
-- TOC entry 5064 (class 0 OID 0)
-- Dependencies: 232
-- Name: prescricao_id_prescricao_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.prescricao_id_prescricao_seq OWNED BY public.prescricao.id_prescricao;


--
-- TOC entry 229 (class 1259 OID 16561)
-- Name: solicitacao_entrada; Type: TABLE; Schema: public; Owner: postgres
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


ALTER TABLE public.solicitacao_entrada OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 16560)
-- Name: solicitacao_entrada_id_solicitacao_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.solicitacao_entrada_id_solicitacao_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.solicitacao_entrada_id_solicitacao_seq OWNER TO postgres;

--
-- TOC entry 5065 (class 0 OID 0)
-- Dependencies: 228
-- Name: solicitacao_entrada_id_solicitacao_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.solicitacao_entrada_id_solicitacao_seq OWNED BY public.solicitacao_entrada.id_solicitacao;


--
-- TOC entry 220 (class 1259 OID 16390)
-- Name: usuario; Type: TABLE; Schema: public; Owner: postgres
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


ALTER TABLE public.usuario OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16389)
-- Name: usuario_id_usuario_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.usuario_id_usuario_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.usuario_id_usuario_seq OWNER TO postgres;

--
-- TOC entry 5066 (class 0 OID 0)
-- Dependencies: 219
-- Name: usuario_id_usuario_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.usuario_id_usuario_seq OWNED BY public.usuario.id_usuario;


--
-- TOC entry 4845 (class 2604 OID 16424)
-- Name: alerta id_alerta; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alerta ALTER COLUMN id_alerta SET DEFAULT nextval('public.alerta_id_alerta_seq'::regclass);


--
-- TOC entry 4846 (class 2604 OID 16521)
-- Name: instituicao id_instituicao; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.instituicao ALTER COLUMN id_instituicao SET DEFAULT nextval('public.instituicao_id_instituicao_seq'::regclass);


--
-- TOC entry 4844 (class 2604 OID 16408)
-- Name: medicacao id_medicamento; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.medicacao ALTER COLUMN id_medicamento SET DEFAULT nextval('public.medicacao_id_medicamento_seq'::regclass);


--
-- TOC entry 4853 (class 2604 OID 16588)
-- Name: paciente id_paciente; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paciente ALTER COLUMN id_paciente SET DEFAULT nextval('public.paciente_id_paciente_seq'::regclass);


--
-- TOC entry 4859 (class 2604 OID 16616)
-- Name: prescricao id_prescricao; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prescricao ALTER COLUMN id_prescricao SET DEFAULT nextval('public.prescricao_id_prescricao_seq'::regclass);


--
-- TOC entry 4850 (class 2604 OID 16564)
-- Name: solicitacao_entrada id_solicitacao; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.solicitacao_entrada ALTER COLUMN id_solicitacao SET DEFAULT nextval('public.solicitacao_entrada_id_solicitacao_seq'::regclass);


--
-- TOC entry 4843 (class 2604 OID 16393)
-- Name: usuario id_usuario; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario ALTER COLUMN id_usuario SET DEFAULT nextval('public.usuario_id_usuario_seq'::regclass);


--
-- TOC entry 5045 (class 0 OID 16421)
-- Dependencies: 224
-- Data for Name: alerta; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.alerta (id_alerta, data_hora_disparo, status, id_medicamento) FROM stdin;
\.


--
-- TOC entry 5047 (class 0 OID 16518)
-- Dependencies: 226
-- Data for Name: instituicao; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.instituicao (id_instituicao, nome, tipo, email, senha, data_criacao, localizacao, biografia) FROM stdin;
1	teste	Hospital	teste@gmail.com	ZaeJ74lWYi6RwYsWf135Mw==:TanZ/SGphad02bwxdX39fUuI3kX7AG0wjwhXlHsYetk=	2026-09-28 17:19:57.604014	\N	\N
2	raio de luz	Clínica	raiodeluz@gmail.com	v8f20uLUEkrghBAQlT1vkQ==:95FYTGpEBKIo3HIYOawPHHZgrhQnj0ID5B4o6h9QWzc=	2026-09-28 18:36:14.892497	aqui	oie
\.


--
-- TOC entry 5043 (class 0 OID 16405)
-- Dependencies: 222
-- Data for Name: medicacao; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.medicacao (id_medicamento, dosagem, intervalo_horas, duracao_dias, id_usuario, nome_medicamento) FROM stdin;
\.


--
-- TOC entry 5048 (class 0 OID 16535)
-- Dependencies: 227
-- Data for Name: membro_instituicao; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.membro_instituicao (id_instituicao, id_usuario, papel, data_entrada) FROM stdin;
1	1	dono	2026-09-28 17:19:57.604014
1	2	membro	2026-09-28 18:30:17.993573
1	3	membro	2026-09-28 18:32:17.998618
1	4	membro	2026-09-28 18:35:10.5606
2	5	dono	2026-09-28 18:36:14.892497
2	6	membro	2026-09-28 19:10:03.717341
2	7	membro	2026-09-28 19:19:04.792299
2	8	membro	2026-09-28 19:21:05.632309
2	9	membro	2026-09-28 19:30:18.328827
\.


--
-- TOC entry 5052 (class 0 OID 16585)
-- Dependencies: 231
-- Data for Name: paciente; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.paciente (id_paciente, nome, idade, genero, peso, estado_civil, tem_filhos, quantidade_filhos, doencas_respiratorias, quais_doencas_respiratorias, doencas_cardiovasculares, quais_doencas_cardiovasculares, tem_alergias, quais_alergias, informacoes_extras, anotacoes, id_usuario, data_cadastro) FROM stdin;
1	lucas	30	Mulher Cis	76.00	solteiro	f	\N	f	\N	f	\N	f	\N	Digite informações extras	Digite suas anotações	1	2026-09-28 17:43:01.292667
\.


--
-- TOC entry 5054 (class 0 OID 16613)
-- Dependencies: 233
-- Data for Name: prescricao; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.prescricao (id_prescricao, nome_paciente, tecnico_responsavel, remedios, doses, horarios, data_cadastro, id_paciente) FROM stdin;
1	luan	an	paracetamol	45mg	09:08	2026-09-28 17:56:00.606343	\N
2	tt	ana	dipirona	89mg	19:32	2026-09-28 19:31:25.810118	\N
\.


--
-- TOC entry 5050 (class 0 OID 16561)
-- Dependencies: 229
-- Data for Name: solicitacao_entrada; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.solicitacao_entrada (id_solicitacao, id_instituicao, nome_solicitante, email_solicitante, mensagem, status, data_solicitacao) FROM stdin;
1	1	lucas	lucas@gmail.com	oi	pendente	2026-09-28 17:23:27.784525
2	1	ana	ana@gmail.com	oi	pendente	2026-09-28 17:26:18.53878
3	1	luan	luan@gmail.com	lu	pendente	2026-09-28 17:43:44.309228
4	1	oi	oi@gmail.com	oi	pendente	2026-09-28 17:51:43.191915
5	1	an	an@gmail.com	an	pendente	2026-09-28 17:55:29.178024
6	1	ruan	ruan@gmail.com	r	pendente	2026-09-28 17:59:51.219548
7	1	leti	leti@gmail.com	oi	pendente	2026-09-28 18:11:46.559918
8	1	r	r@gmail.com	3	pendente	2026-09-28 18:12:19.157978
9	1	i	i@gmail.com	3	pendente	2026-09-28 18:20:38.385277
10	1	oie	oie@gmail.com	oie	aprovada	2026-09-28 18:30:17.993573
11	1	io	io@gmail.com	r	aprovada	2026-09-28 18:32:17.998618
12	1	y	y@gmail.com	tt	aprovada	2026-09-28 18:35:10.5606
13	2	ry	ry@gmail.com	3	aprovada	2026-09-28 19:10:03.717341
14	2	er	er@gmail.com	er	aprovada	2026-09-28 19:19:04.792299
15	2	we	we@gmail.com	we	aprovada	2026-09-28 19:21:05.632309
16	2	ww	ww@gmail.com	ww	aprovada	2026-09-28 19:30:18.328827
\.


--
-- TOC entry 5041 (class 0 OID 16390)
-- Dependencies: 220
-- Data for Name: usuario; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usuario (id_usuario, nome, email, senha, data_nascimento, genero, biografia) FROM stdin;
1	lu	lu@gmail.com	CSwm1xksTBY1SshCuwaCYw==:ZvEA5IbiHS4mJkoRtz+4e153NSZyIj8hArK3xRGHo6Q=	2008-09-27	\N	\N
2	oie	oie@gmail.com	3844803b18794cca9f5e3988858b3d5b	\N	\N	\N
3	io	io@gmail.com	3f8fab476c7d4abfaba279e6169a417c	\N	\N	\N
4	y	y@gmail.com	86f6546c5b1f4afd951243d9b791bfa0	\N	\N	\N
5	lucas andrade	lucas@gmail.com	kBOSZ640cHjff9Nq5onPtw==:RKAavJonO8mCdlgTJP8OjCKRg7yPCd7wVY0WbLZt7kw=	2008-09-28	\N	\N
6	lukinhas	lukinhas02123@gmail.com	AZAREfhXdFoSlELeD3CZYw==:rYTIOrQBeHQvYbQ8h69CfKXpfUMd15hphPscGtfTflE=	\N	masculine	oie!
7	er	er@gmail.com	nV8M1gqkKA52sLQBT2Yc3A==:1ehMvb4XJOnFdgFmnKIqSRjZ44yPW4J/qX5pSCMrmK4=	\N	masculino	Técnico
8	we	we@gmail.com	Ls4zIvGCqCRCauD+KwCg9A==:Lu5/fAEi2hPXcbSNDo9rIXIc5jD+dm0BlodmOAcvLbY=	\N	masuclino	er
9	ww	ww@gmail.com	hVItK3jqz7KCKowyygZp9Q==:RacO+dr+wgDWPWLbSSjs4/BGFcuPeq3sPMmWQuEztgk=	\N	\N	\N
\.


--
-- TOC entry 5067 (class 0 OID 0)
-- Dependencies: 223
-- Name: alerta_id_alerta_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.alerta_id_alerta_seq', 1, false);


--
-- TOC entry 5068 (class 0 OID 0)
-- Dependencies: 225
-- Name: instituicao_id_instituicao_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.instituicao_id_instituicao_seq', 2, true);


--
-- TOC entry 5069 (class 0 OID 0)
-- Dependencies: 221
-- Name: medicacao_id_medicamento_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.medicacao_id_medicamento_seq', 1, false);


--
-- TOC entry 5070 (class 0 OID 0)
-- Dependencies: 230
-- Name: paciente_id_paciente_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.paciente_id_paciente_seq', 1, true);


--
-- TOC entry 5071 (class 0 OID 0)
-- Dependencies: 232
-- Name: prescricao_id_prescricao_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.prescricao_id_prescricao_seq', 2, true);


--
-- TOC entry 5072 (class 0 OID 0)
-- Dependencies: 228
-- Name: solicitacao_entrada_id_solicitacao_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.solicitacao_entrada_id_solicitacao_seq', 16, true);


--
-- TOC entry 5073 (class 0 OID 0)
-- Dependencies: 219
-- Name: usuario_id_usuario_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.usuario_id_usuario_seq', 9, true);


--
-- TOC entry 4871 (class 2606 OID 16430)
-- Name: alerta alerta_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alerta
    ADD CONSTRAINT alerta_pkey PRIMARY KEY (id_alerta);


--
-- TOC entry 4873 (class 2606 OID 16534)
-- Name: instituicao instituicao_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.instituicao
    ADD CONSTRAINT instituicao_email_key UNIQUE (email);


--
-- TOC entry 4875 (class 2606 OID 16532)
-- Name: instituicao instituicao_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.instituicao
    ADD CONSTRAINT instituicao_pkey PRIMARY KEY (id_instituicao);


--
-- TOC entry 4869 (class 2606 OID 16414)
-- Name: medicacao medicacao_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.medicacao
    ADD CONSTRAINT medicacao_pkey PRIMARY KEY (id_medicamento);


--
-- TOC entry 4877 (class 2606 OID 16548)
-- Name: membro_instituicao membro_instituicao_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.membro_instituicao
    ADD CONSTRAINT membro_instituicao_pkey PRIMARY KEY (id_instituicao, id_usuario);


--
-- TOC entry 4883 (class 2606 OID 16606)
-- Name: paciente paciente_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paciente
    ADD CONSTRAINT paciente_pkey PRIMARY KEY (id_paciente);


--
-- TOC entry 4885 (class 2606 OID 16628)
-- Name: prescricao prescricao_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prescricao
    ADD CONSTRAINT prescricao_pkey PRIMARY KEY (id_prescricao);


--
-- TOC entry 4880 (class 2606 OID 16577)
-- Name: solicitacao_entrada solicitacao_entrada_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.solicitacao_entrada
    ADD CONSTRAINT solicitacao_entrada_pkey PRIMARY KEY (id_solicitacao);


--
-- TOC entry 4865 (class 2606 OID 16403)
-- Name: usuario usuario_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_email_key UNIQUE (email);


--
-- TOC entry 4867 (class 2606 OID 16401)
-- Name: usuario usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id_usuario);


--
-- TOC entry 4878 (class 1259 OID 16559)
-- Name: um_dono_por_instituicao; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX um_dono_por_instituicao ON public.membro_instituicao USING btree (id_instituicao) WHERE (papel = 'dono'::text);


--
-- TOC entry 4881 (class 1259 OID 16583)
-- Name: um_pedido_pendente; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX um_pedido_pendente ON public.solicitacao_entrada USING btree (id_instituicao, lower(email_solicitante)) WHERE (status = 'pendente'::text);


--
-- TOC entry 4887 (class 2606 OID 16431)
-- Name: alerta alerta_id_medicamento_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alerta
    ADD CONSTRAINT alerta_id_medicamento_fkey FOREIGN KEY (id_medicamento) REFERENCES public.medicacao(id_medicamento);


--
-- TOC entry 4886 (class 2606 OID 16415)
-- Name: medicacao medicacao_id_usuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.medicacao
    ADD CONSTRAINT medicacao_id_usuario_fkey FOREIGN KEY (id_usuario) REFERENCES public.usuario(id_usuario);


--
-- TOC entry 4888 (class 2606 OID 16549)
-- Name: membro_instituicao membro_instituicao_id_instituicao_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.membro_instituicao
    ADD CONSTRAINT membro_instituicao_id_instituicao_fkey FOREIGN KEY (id_instituicao) REFERENCES public.instituicao(id_instituicao);


--
-- TOC entry 4889 (class 2606 OID 16554)
-- Name: membro_instituicao membro_instituicao_id_usuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.membro_instituicao
    ADD CONSTRAINT membro_instituicao_id_usuario_fkey FOREIGN KEY (id_usuario) REFERENCES public.usuario(id_usuario);


--
-- TOC entry 4891 (class 2606 OID 16607)
-- Name: paciente paciente_id_usuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paciente
    ADD CONSTRAINT paciente_id_usuario_fkey FOREIGN KEY (id_usuario) REFERENCES public.usuario(id_usuario);


--
-- TOC entry 4892 (class 2606 OID 16672)
-- Name: prescricao prescricao_id_paciente_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prescricao
    ADD CONSTRAINT prescricao_id_paciente_fkey FOREIGN KEY (id_paciente) REFERENCES public.paciente(id_paciente);


--
-- TOC entry 4890 (class 2606 OID 16578)
-- Name: solicitacao_entrada solicitacao_entrada_id_instituicao_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.solicitacao_entrada
    ADD CONSTRAINT solicitacao_entrada_id_instituicao_fkey FOREIGN KEY (id_instituicao) REFERENCES public.instituicao(id_instituicao);


-- Completed on 2026-09-28 19:52:08

--
-- PostgreSQL database dump complete
--

\unrestrict Ym0qRZP6mcH5ruVOx4WCDBsrXsR9QGQogWMqt89NbmkbdAl4OLfVNRgWzxGtXtd

