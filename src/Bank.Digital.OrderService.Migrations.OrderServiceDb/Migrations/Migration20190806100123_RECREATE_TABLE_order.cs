using FluentMigrator;

namespace Bank.Digital.OrderService.Migrations.OrderServiceDb.Migrations
{
    //ReSharper disable once InconsistentNaming

    [Migration(20190806100123)]
    public class Migration20190806100123_RECREATE_TABLE_order : ForwardOnlyMigration
    {
        /// <inheritdoc />
        public override void Up()
        {
            Execute.Sql(@"
    
                DROP TABLE IF EXISTS ""order""  CASCADE;
                CREATE TABLE ""order""
                (
                    id uuid NOT NULL,
                    applicant_id integer NOT NULL,
                    state varchar(32) NOT NULL,
                    date_add timestamp with time zone NOT NULL,
                    product_id integer NOT NULL,
                    CONSTRAINT order_pkey PRIMARY KEY (id)
                )
                WITH (
                    OIDS = FALSE
                );

                DROP TABLE IF EXISTS ""order_context""  CASCADE;
                CREATE TABLE ""order_context""
                (
                    order_id uuid NOT NULL,
                    ip character varying(64),
                    utm text,
                    google_analytics_id text,
                    region character varying(256),
                    browser text,
                    browser_user_agent text,
                    CONSTRAINT order_context_pkey PRIMARY KEY(order_id),
                    CONSTRAINT fk_order_context_order FOREIGN KEY(order_id) REFERENCES public.order(id) MATCH SIMPLE
                    ON UPDATE CASCADE
                    ON DELETE CASCADE
                )
                WITH(
                    OIDS = FALSE
                );
            ");
        }
    }
}