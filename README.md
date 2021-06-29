# Simply webhook for startupjob.

Description of is https://www.startupjobs.cz/dev/webhooks 
Store data of candidate to DB: appliciants, JobAdResponses, documents.

Simply version use minimal sources and SQLite DB.
Full version use MVC model and MS SQL DB.

* For develop run in mode startupjob no IIS.
* For debugging use Chrome extension: YARC!
  To payload fill JSON
  {
    "date":"2017-09-11T18:19:15+02:00",
    "candidateID":12345,
    "offerID":1234,
    "name":"Mr. Shark",
    "position":"Webhook developer",
    "why":"<p>Hello from StartupJobs. This is not a real candidate, just testing your webhook.</p>",
    "phone":"+420 725 875 752",
    "email":"dev@startupjobs.cz",
    "details":"https://www.startupjobs.cz/",
    "linkedin":"https://linkedin.com/",
    "internalPositionName":"JOB1",
    "files":["https://www.startupjobs.cz/favicon.ico"],
    "gdpr_accepted": true,
    "test":true
  }
