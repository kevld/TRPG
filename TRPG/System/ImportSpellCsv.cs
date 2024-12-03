using TRPG.Enums;
using TRPG.Models;

namespace TRPG.System
{
    public class ImportSpellCsv
    {
        private readonly Db _db;

        public ImportSpellCsv(Db db)
        {
            _db = db;
        }

        public void ImportCsvFile(string filePath)
        {
            List<Spell> records = new();
            foreach (var line in File.ReadLines(filePath))
            {
                var values = line.Split(';');
                if (values[0].Contains("Id"))
                    continue;

                records.Add(new Spell()
                {
                    Id = int.Parse(values[0]),
                    Year = int.Parse(values[1]),
                    Name = values[2],
                    Description = values[3],
                    Type = (SpellType)int.Parse(values[4])
                });
            }

            int dbSpellNumber = _db.Spells.Count();

            if (records.Count() != dbSpellNumber)
            {
                List<int> existingSpellIds = _db.Spells.Select(s => s.Id).ToList();
                List<Spell> spellsToAdd = records.Where(x => !existingSpellIds.Contains(x.Id)).ToList();
                foreach (Spell spell in records)
                {
                    _db.Spells.Add(spell);
                }
                _db.SaveChanges();
            }

        }
    }
}
