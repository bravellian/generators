CREATE TABLE [pw].[BravillianTenant] (
    [BravillianTenantId] BIGINT NOT NULL,
    [Name]              NVARCHAR (200)   NOT NULL,
    [OrganizationId]    BIGINT NOT NULL,
    CONSTRAINT [PK_BravillianTenant] PRIMARY KEY ([BravillianTenantId] ASC),
    CONSTRAINT [FK_pw_BravillianTenant_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [pw].[Organization] ([OrganizationId])
);

