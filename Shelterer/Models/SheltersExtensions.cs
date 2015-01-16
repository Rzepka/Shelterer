using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Shelterer.Models;

namespace SheltererExtensionMethods
{
    public static class SheltersExtensions
    {
        //non async for initialization
        public static void SaveRecordSync(this SheltersContext db, DbRecord record, string author, string message)
        {
            //get all properties
            var properties = record.GetType().GetProperties();
            //save all modified properties
            foreach (var p in properties)
            {
                db.StoreFieldSync(p, record, author, message);
            }
            db.SaveChanges();
        }

        public static void StoreFieldSync(this SheltersContext db, PropertyInfo info, DbRecord record, string author, string message)
        {
            var tableDescr = DbRecord.TableFieldIds[record.GetType().Name];
            int tableId = tableDescr.Item1;
            int fieldId;
            try
            {
                fieldId = tableDescr.Item2[info.Name];
            }
            catch (KeyNotFoundException)
            {
                return;
            }

            Type type = info.PropertyType;
            if (type.Name.Contains("Nullable"))
            {
                type = Nullable.GetUnderlyingType(type);
            }
            int typeId;
            try
            {
                typeId = DbRecord.DataTypeIds[type.Name];
            }
            catch (Exception)
            {
                return;
                //throw;
            }

            var fieldValue = info.GetValue(record);
            //if (recordValue == null)
            //{
            //    return;
            //}
            //string value = recordValue.ToString();
            string value = (fieldValue == null) ? "" : fieldValue.ToString();

            FieldInstant oldField = null;
            try
            {
                oldField = db.FieldInstants.First(r =>
                                   r.TableId == tableId &&
                                   r.RecordId == record.Id &&
                                   r.FieldId == fieldId &&
                                   r.DataTypeId == typeId &&
                                   r.EndDate == null);
            }
            catch (InvalidOperationException)
            { }
            catch (Exception)
            {
                throw;
            }

            if (oldField == null)
            {
                var newField = new FieldInstant()
                {
                    TableId = tableId,
                    RecordId = record.Id,
                    FieldId = fieldId,
                    DataTypeId = typeId,
                    Value = value,
                    Author = author,
                    Message = message,
                    StartDate = DateTime.Now
                };
                db.FieldInstants.Add(newField);
            }
            else if (oldField.Value != value)
            {
                oldField.EndDate = DateTime.Now;
                db.Entry(oldField).State = System.Data.Entity.EntityState.Modified;

                var newField = new FieldInstant()
                {
                    TableId = tableId,
                    RecordId = record.Id,
                    FieldId = fieldId,
                    DataTypeId = typeId,
                    Value = value,
                    Author = author,
                    Message = message,
                    StartDate = DateTime.Now
                };
                db.FieldInstants.Add(newField);
            }
        }









        //----------------------------------------------
        //async

        public static async Task SaveRecord(this SheltersContext db, DbRecord record, string author, string message)
        {
            //get all properties
            var properties = record.GetType().GetProperties();
            //save all modified properties
            foreach (var p in properties)
            {
                await db.StoreField(p, record, author, message);
            }
            await db.SaveChangesAsync();
        }


        public static async Task StoreField(this SheltersContext db, PropertyInfo info, DbRecord record, string author, string message)
        {
            var tableDescr = DbRecord.TableFieldIds[record.GetType().Name];
            int tableId = tableDescr.Item1;
            int fieldId;
            try
            {
                fieldId = tableDescr.Item2[info.Name];
            }
            catch (KeyNotFoundException)
            {
                return;
            }

            Type type = info.PropertyType;
            if (type.Name.Contains("Nullable"))
            {
                type = Nullable.GetUnderlyingType(type);
            }
            int typeId;
            try
            {
                typeId = DbRecord.DataTypeIds[type.Name];
            }
            catch (Exception)
            {
                return;
                //throw;
            }

            var fieldValue = info.GetValue(record);
            //if (recordValue == null)
            //{
            //    return;
            //}
            //string value = recordValue.ToString();
            string value = (fieldValue == null) ? "" : fieldValue.ToString();

            FieldInstant oldField = null;
            try
            {
                oldField = await db.FieldInstants.FirstAsync(r =>
                                   r.TableId == tableId &&
                                   r.RecordId == record.Id &&
                                   r.FieldId == fieldId &&
                                   r.DataTypeId == typeId &&
                                   r.EndDate == null);
            }
            catch (InvalidOperationException)
            { }
            catch (Exception)
            {
                throw;
            }

            if (oldField == null)
            {
                var newField = new FieldInstant()
                {
                    TableId = tableId,
                    RecordId = record.Id,
                    FieldId = fieldId,
                    DataTypeId = typeId,
                    Value = value,
                    Author = author,
                    Message = message,
                    StartDate = DateTime.Now
                };
                db.FieldInstants.Add(newField);
            }
            else if (oldField.Value != value)
            {
                oldField.EndDate = DateTime.Now;
                db.Entry(oldField).State = System.Data.Entity.EntityState.Modified;

                var newField = new FieldInstant()
                {
                    TableId = tableId,
                    RecordId = record.Id,
                    FieldId = fieldId,
                    DataTypeId = typeId,
                    Value = value,
                    Author = author,
                    Message = message,
                    StartDate = DateTime.Now
                };
                db.FieldInstants.Add(newField);
            }
        }

        //public static async Task StoreField(this SheltersContext db, PropertyInfo info, DbRecord record, string author, string message)
        //{
        //    var fieldIds = record.GetFieldIds();
        //    int fieldId;
        //    try
        //    {
        //        fieldId = fieldIds[info.Name];
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return;
        //    }

        //    Type type = info.PropertyType;
        //    if (type.Name.Contains("Nullable"))
        //    {
        //        type = Nullable.GetUnderlyingType(type);
        //    }
        //    int typeId;
        //    try
        //    {
        //        typeId = DbRecord.DataTypeIds[type.Name];
        //    }
        //    catch (Exception)
        //    {
        //        return;
        //        //throw;
        //    }

        //    var fieldValue = info.GetValue(record);
        //    //if (recordValue == null)
        //    //{
        //    //    return;
        //    //}
        //    //string value = recordValue.ToString();
        //    string value = (fieldValue == null) ? "" : fieldValue.ToString();

        //    var recordType = record.GetType().Name;
        //    int tableId = DbRecord.TableIds[recordType];

        //    FieldInstant oldField = null;
        //    try
        //    {
        //        oldField = await db.FieldInstants.FirstAsync(r =>
        //                           r.TableId == tableId &&
        //                           r.RecordId == record.Id &&
        //                           r.FieldId == fieldId &&
        //                           r.DataTypeId == typeId &&
        //                           r.EndDate == null);
        //    }
        //    catch (InvalidOperationException)
        //    { }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    if (oldField == null)
        //    {
        //        var newField = new FieldInstant()
        //        {
        //            TableId = tableId,
        //            RecordId = record.Id,
        //            FieldId = fieldId,
        //            DataTypeId = typeId,
        //            Value = value,
        //            Author = author,
        //            Message = message,
        //            StartDate = DateTime.Now
        //        };
        //        db.FieldInstants.Add(newField);
        //    }
        //    else if (oldField.Value != value)
        //    {
        //        oldField.EndDate = DateTime.Now;
        //        db.Entry(oldField).State = System.Data.Entity.EntityState.Modified;

        //        var newField = new FieldInstant()
        //        {
        //            TableId = tableId,
        //            RecordId = record.Id,
        //            FieldId = fieldId,
        //            DataTypeId = typeId,
        //            Value = value,
        //            Author = author,
        //            Message = message,
        //            StartDate = DateTime.Now
        //        };
        //        db.FieldInstants.Add(newField);
        //    }
        //}


        public static async Task<Tuple<DbRecord, string>> RetriveProperty(this SheltersContext db, FieldInstant field, string author, string message)//int id
        {
            //FieldInstant field = await db.FieldInstants.FindAsync(id);

            string tableName = DbRecord.TableFieldIds.First(td => td.Value.Item1 == field.TableId).Key;
            var tableDescr = DbRecord.TableFieldIds[tableName];
            string fieldName = tableDescr.Item2.First(r => r.Value == field.FieldId).Key;            

            string valueType = DbRecord.DataTypeIds.First(r => r.Value == field.DataTypeId).Key;
            var value = Convert.ChangeType(field.Value, Type.GetType("System." + valueType));

            var contextProperty = tableDescr.Item3;
            dynamic tableContext = contextProperty.GetValue(db);
            DbRecord dbRecord = tableContext.Find(field.RecordId);
            
            var recordType = Type.GetType("Shelterer.Models." + tableName);
            var prop = recordType.GetProperty(fieldName);
            prop.SetValue(dbRecord, value);

            db.Entry(dbRecord).State = EntityState.Modified;

            FieldInstant oldField = null;
            try
            {
                oldField = await db.FieldInstants.FirstAsync(r =>
                                   r.TableId == field.TableId &&
                                   r.RecordId == field.RecordId &&
                                   r.FieldId == field.FieldId &&
                                   r.DataTypeId == field.DataTypeId &&
                                   r.EndDate == null);
            }
            catch (InvalidOperationException)
            { }
            catch (Exception)
            {
                throw;
            }

            oldField.EndDate = DateTime.Now;
            db.Entry(oldField).State = System.Data.Entity.EntityState.Modified;

            var newField = new FieldInstant()
            {
                TableId = field.TableId,
                RecordId = field.RecordId,
                FieldId = field.FieldId,
                DataTypeId = field.DataTypeId,
                Value = field.Value,
                Author = author,
                Message = message,
                StartDate = DateTime.Now
            };
            db.FieldInstants.Add(newField);

            await db.SaveChangesAsync();
            return Tuple.Create(dbRecord, tableName);
        }
        

        //public static async Task RetriveProperty(this SheltersContext db, int id)//FieldInstant field
        //{
        //    FieldInstant field = await db.FieldInstants.FindAsync(id);

        //    string tableName = DbRecord.TableIds.First(r => r.Value == field.TableId).Key;
        //    var recordType = Type.GetType("Shelterer.Models." + tableName);

        //    DbRecord dbRecord = (DbRecord)recordType.GetConstructor(new Type[]{}).Invoke(new object[]{});
        //    string fieldName = dbRecord.GetFieldIds().First(r => r.Value == field.FieldId).Key;
             
        //    string valueType = DbRecord.DataTypeIds.First(r => r.Value == field.DataTypeId).Key;
        //    var value = Convert.ChangeType(field.Value, Type.GetType("System." + valueType));


        //    var contextProperties = typeof(SheltersContext).GetProperties();
        //    PropertyInfo contextProperty = contextProperties.First(
        //        p => p.GetValue(db).GetType().GetGenericArguments()[0] == recordType );

        //    dynamic tableContext = contextProperty.GetValue(db);
        //    dbRecord = tableContext.Find(field.RecordId);

        //    recordType.GetProperty(fieldName).SetValue(dbRecord, value);

        //    db.Entry(dbRecord).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //}


    }
}