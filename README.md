This project serves for managing room reservations. The page allows access only to authorized users through a login.
It is possible to add new clients to the database, create a new reservation, and view all existing ones.
For each reservation, additional services can be added, and at checkout, all reservation details and the fee to be paid by the client are displayed.
Additionally, two searches can be performed using asynchronous calls as requested by the task.
I focused more on functionality rather than aesthetics to give more space to the implementation of the newly learned features.


Questo progetto serve per la gestione delle prenotazioni delle camere. La pagina permette di accedere solo agli utenti autorizzati tramite un Login.
E' possibile inserire dei nuovi clienti all'interno del database dell'albero, è possibile creare una nuova prenotazione e visualizzare tutte quelle presenti. 
Per ogni prenotazione, si possono aggiungere dei servizi addizionali e nel momento del checkout, vengono visualizzati tutti i dettagli della prenotazione e la tariffa da pagare per il cliente.
Inoltre è possibile effettuare due ricerche con delle chiamate asincrone come richiesto dal compito. 
Ho dato maggiore attenzione alle funzionalità, piuttosto che all'estetica, per dare più spazio all'implementazione delle nuove funzionalità appena imparate.

Qui sono contenute le query necessarie per creare le tabelle del database:

CREATE TABLE Camere (
    IdCamera INT PRIMARY KEY,
    NumeroCamera INT NOT NULL,
    Descrizione NVARCHAR(100) NOT NULL,
    Tipologia NVARCHAR(50) NOT NULL,
    Prezzo DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Clienti (
    IdCliente INT PRIMARY KEY,
    CodiceFiscale NVARCHAR(16) NOT NULL,
    Cognome NVARCHAR(50) NOT NULL,
    Nome NVARCHAR(50) NOT NULL,
    Città NVARCHAR(50) NOT NULL,
    Provincia NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Telefono NVARCHAR(20),
    Cellulare NVARCHAR(20) NOT NULL
);

CREATE TABLE Prenotazioni (
    IdPrenotazione INT PRIMARY KEY,
    DataPrenotazione DATETIME NOT NULL,
    Anno INT NOT NULL,
    DataArrivo DATETIME NOT NULL,
    DataPartenza DATETIME NOT NULL,
    CaparraConfirmatoria DECIMAL(10, 2) NOT NULL,
    TariffaApplicata DECIMAL(10, 2) NOT NULL,
    TipoSoggiorno NVARCHAR(50) NOT NULL,
    IdCliente INT NOT NULL,
    IdCamera INT NOT NULL,
    TotaleDaPagare DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clienti(IdCliente),
    FOREIGN KEY (IdCamera) REFERENCES Camere(IdCamera)
);

CREATE TABLE Servizi (
    IdServizio INT PRIMARY KEY,
    Descrizione NVARCHAR(100) NOT NULL,
    Prezzo DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Servizi_Prenotazioni {
    IdPrenotazione INT NOT NULL,
    IdServizio INT NOT NULL,
    DataServizio DATETIME NOT NULL,
    Quantità INT NOT NULL,
    FOREIGN KEY (IdPrenotazione) REFERENCES Prenotazioni(IdPrenotazione),
    FOREIGN KEY (IdServizio) REFERENCES Servizi(IdServizio)
);

CREATE TABLE Utenti (
    IdUtente INT PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

Le uniche query necessarie per popolare le tabelle sono: 
INSERT INTO Utenti (Username, Password)
VALUES ('admin', 'asd');

INSERT INTO Servizi (Descrizione, Prezzo)
VALUES ('Wifi', 10),
       ('Culla', 15),
       ('Minibar', 20),
       ('Sky', 8),
       ('Colazione in camera', 12);

INSERT INTO Camere (NumeroCamera, Descrizione, Tipologia, Prezzo)
VALUES 
(101, 'Camera Singola', 'Singola', 50.00),
(102, 'Camera Matrimoniale', 'Matrimoniale', 80.00),
(103, 'Camera Doppia', 'Doppia', 90.00),
(104, 'Camera Tripla', 'Tripla', 120.00),
(105, 'Suite', 'Suite', 150.00);

