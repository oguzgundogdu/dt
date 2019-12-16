CREATE TABLE public.t_users(
user_id serial,
user_name varchar(50),
user_password varchar(500),
status SMALLINT,
CONSTRAINT t_users_pkey PRIMARY KEY(user_id)
)WITH (fillfactor = 50, oids = false);

CREATE TABLE public.t_articles(
article_id serial,
article_title varchar(255) NOT NULL,
article_content varchar NOT NULL,
creation_time timestamp NOT NULL DEFAULT now(),
last_modified_time timestamp NOT NULL,
created_by int,
status SMALLINT,
CONSTRAINT t_articles_pkey PRIMARY KEY(article_id)
)WITH (fillfactor = 75, oids = false);

CREATE TABLE public.t_comments(
comment_id serial,
article_id int,
comment_content varchar(5000) NOT NULL,
creation_time timestamp NOT NULL DEFAULT now(),
created_by int,
status SMALLINT,
CONSTRAINT t_comments_pkey PRIMARY KEY(comment_id),
CONSTRAINT t_comments_t_articles_fkey FOREIGN KEY (article_id) REFERENCES public.t_articles(article_id)
)WITH (fillfactor = 99, oids = false);

