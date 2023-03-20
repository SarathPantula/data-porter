create table if not exists azure_blob_reference(
	id uuid not null constraint pk_tenant_id primary key,
	name varchar(200) not null
);