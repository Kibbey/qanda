# qanda
Testing out .net core 2.1 application.

Three layer application with a Domain Driven flavor Qanda.API (folder is `qanda` contains controllers), Qanda.Domain (business logic), Qanda.Repository (data access layer).

Data layer is just in memory for easy of running but could substitute a web service, sql, or noSql implementation.

If doing a noSql implemtation would suggest a document per QA session (leaving out timestamps). May change over id's to uuid from int:
```json
{
  id: uuid,
  start: dateTime,
  end: dateTime,
  host: string,
  questions:[
    {
      id: uuid,
      question: string,
      answer: string,
      imageUrl: string,
      answeredBy: string
  ]
}
```
Sql implementation may look like:

QA Table
- id: int PK
- start: dateTime
- end: dateTime
- host: nvarchar

Question Table
- id: int PK
- qaId: int FK (QA)
- question: nvarchar
- answer: nvarchar
- imageUrl: nvarchar
- answeredBy: nvarchar
