CREATE TABLE IF NOT EXISTS public."Profiles"
(
    "UserId" bigint NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 100 ),
    "Version" uuid NOT NULL,
    "UserName" text COLLATE pg_catalog."default" NOT NULL,
    "Password" text COLLATE pg_catalog."default" NOT NULL,
    "DisplayName" text COLLATE pg_catalog."default" NOT NULL,
    "AvatarUrl" text COLLATE pg_catalog."default" NOT NULL,
    "Phone" text COLLATE pg_catalog."default" NOT NULL,
    "Email" text COLLATE pg_catalog."default" NOT NULL,
    PRIMARY KEY ("UserId" HASH)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE INDEX "Profiles_UserName_index"
    ON public."Profiles" USING btree
        ("UserName" ASC NULLS LAST);

ALTER TABLE IF EXISTS public."Profiles"
    ADD CONSTRAINT "Profiles_UserName_unique" UNIQUE ("UserName");

ALTER TABLE IF EXISTS public."Profiles"
    OWNER to yugabyte;
GRANT ALL ON TABLE public."Profiles" TO cecochat_dev;
GRANT ALL ON TABLE public."Profiles" TO yugabyte;
