create table if not exists azure_blob_reference(
	id uuid not null constraint pk_tenant_id primary key,
	file_name varchar(200) not null,
	file_size bigint not null,
	file_type varchar(200) not null,
	created_date timestamp not null DEFAULT NOW()
);