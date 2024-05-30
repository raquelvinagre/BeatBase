# BD: Trabalho Prático APF-T

**Grupo**: P4G6
- Diogo Fernandes, MEC: 114137
- Raquel Vinagre, MEC: 113736

# Instructions - TO REMOVE

Este template é flexível.
É sugerido seguir a estrutura, links de ficheiros e imagens, mas adicione ou remova conteúdo sempre que achar necessário.

---

This template is flexible.
It is suggested to follow the structure, file links and images but add more content where necessary.

The files should be organized with the following nomenclature:

- sql\01_ddl.sql: mandatory for DDL
- sql\02_sp_functions.sql: mandatory for Store Procedure, Functions,... 
- sql\03_triggers.sql: mandatory for triggers
- sql\04_db_init.sql: scripts to init the database (i.e. inserts etc.)
- sql\05_any_other_matter.sql: any other scripts.

Por favor remova esta secção antes de submeter.

Please remove this section before submitting.

## Introdução / Introduction
 
Este projeto consiste numa base de dados para uma plataforma de streaming digital de música, de modo a gerir utilizadores, playlists, músicas, artistas e álbuns.
Os utilizadores podem ouvir músicas adicionadas pelos artistas. É possível, ainda, criar playlists personalizadas, e verificar as leaderboards globais.


## ​Análise de Requisitos / Requirements

## DER - Diagrama Entidade Relacionamento/Entity Relationship Diagram

### Versão final/Final version

![DER Diagram!](../BeatBase/diagrams/DER%20UPDATED.png "AnImage")

### APFE 

Descreva sumariamente as melhorias sobre a primeira entrega.

Foi criada a entidade PlaylistSong (com ID da playlist e da música), que foi necessária para adicionar músicas a playlists. Como uma música pode pertencer a mais do que uma playlist, o processo não poderia ser o mesmo que o de adicionar músicas a álbuns (consistia em mudar o ID do álbum como atributo da música para o ID do álbum a que pertença, isto resulta pois uma música só pode pertencer a 0 ou 1 álbuns). 

Agora está especificado que a Global Leaderboard apenas contém músicas.

## ER - Esquema Relacional/Relational Schema

### Versão final/Final Version

![ER Diagram!](../BeatBase/diagrams/ER%20UPDATED.png "AnImage")

### APFE

Descreva sumariamente as melhorias sobre a primeira entrega.

As alterações correspondem às anteriores.

## ​SQL DDL - Data Definition Language

[SQL DDL File](../BeatBase/sql/01_ddl.sql "SQLFileQuestion")

## SQL DML - Data Manipulation Language

Uma secção por formulário.
A section for each form.

### Formulario exemplo/Example Form

![Exemplo Screenshot!](screenshots/screenshot_1.jpg "AnImage")

```sql
-- Show data on the form
SELECT * FROM MY_TABLE ....;

-- Insert new element
INSERT INTO MY_TABLE ....;
```

...

## Normalização/Normalization

Descreva os passos utilizados para minimizar a duplicação de dados / redução de espaço.

Não foi necessário recorrer a normalização, pois todas as tabelas já se encontravam normalizadas.

## Índices/Indexes

Descreva os indices criados. Junte uma cópia do SQL de criação do indice.

Os índices criados foram os seguintes:

- Índice na Song table para o ArtistID
- Índice na Album table para o ArtistID
- Índice na Playlist table para o AuthorID
- Índice na Song table para o Genre
- Índice na Playlist table para o Genre


```sql
-- Create an index to speed

IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'idx_Song_ArtistID')
BEGIN
    DROP INDEX idx_Song_ArtistID ON Song;
END
CREATE INDEX idx_Song_ArtistID ON Song (ArtistID);


IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'idx_Album_ArtistID')
BEGIN
    DROP INDEX idx_Album_ArtistID ON Album;
END
CREATE INDEX idx_Album_ArtistID ON Album (ArtistID);

```

## SQL Programming: Stored Procedures, Triggers, UDF

[SQL SPs and Functions File](sql/02_sp_functions.sql "SQLFileQuestion")

[SQL Triggers File](../BeatBase/sql/03_triggers.sql "SQLFileQuestion")

## Outras notas/Other notes

### Dados iniciais da base de dados/Database init data

[Indexes File](../BeatBase/sql/02_Indexes.sql "SQLFileQuestion")



 