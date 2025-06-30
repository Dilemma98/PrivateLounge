# PrivateLounge

PrivateLounge är en personlig dagboks- och att-göra-app byggd med .NET (backend) och Vue/Vite (frontend).

## Funktioner

- Skriv dagboksinlägg och ladda upp en eller flera bilder per inlägg
- Se, ta bort och hantera dina inlägg
- Att-göra-lista
- Gullig och personlig design

## Teknisk info

- **Backend:** ASP.NET Core, Entity Framework Core, SQLite
- **Frontend:** Vue 3, Vite
- **Bilder:** Laddas upp till `wwwroot/uploads` och visas i inlägg

## Kom igång

1. **Klona repot**
2. **Installera paket**
   - Backend:  
     ```sh
     cd Backend
     dotnet restore
     ```
   - Frontend:  
     ```sh
     cd ../Frontend
     npm install
     ```
3. **Skapa databas**
   - I [Backend](http://_vscodecontentref_/0)-mappen:  
     ```sh
     dotnet ef database update
     ```
4. **Starta appar**
   - Backend:  
     ```sh
     dotnet run
     ```
   - Frontend:  
     ```sh
     npm run dev
     ```
5. **Öppna Frontend**  
   Gå till [http://localhost:5173](http://localhost:5173)

## Notera

- Din databas (`posts.db`) är **inte** med i repot (se [.gitignore](http://_vscodecontentref_/1)). Alla får en egen tom databas vid första körning.
- Bilder du laddar upp sparas endast lokalt på din server.

---

**Byggd av Emma H (och CoPilot)**