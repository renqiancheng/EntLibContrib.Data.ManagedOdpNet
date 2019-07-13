//===============================================================================
// Microsoft patterns & practices Enterprise Library Contribution
// Data Access Application Block
//===============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

namespace EntLibContrib.Data.ManagedOdpNet
{
	/// <summary>
	/// A wrapper to convert data for oracle for the reader.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	public sealed class OracleDataReaderWrapper : MarshalByRefObject, IDataReader, IEnumerable
	{
		#region Fields
		private readonly OracleDataReader innerReader;
		#endregion

		#region Properties
		/// <summary>
		/// Gets a value indicating the depth of nesting for the current row.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The level of nesting.
		/// </returns>
		public int Depth
		{
			get { return InnerReader.Depth; }
		}

		/// <summary>
		/// Gets a value indicating whether the data reader is closed.
		/// </summary>
		/// <value></value>
		/// <returns>true if the data reader is closed; otherwise, false.
		/// </returns>
		public bool IsClosed
		{
			get { return InnerReader.IsClosed; }
		}

		/// <summary>
		/// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.
		/// </returns>
		public int RecordsAffected
		{
			get { return InnerReader.RecordsAffected; }
		}

		/// <summary>
		/// Gets the number of columns in the current row.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.
		/// </returns>
		public int FieldCount
		{
			get { return InnerReader.FieldCount; }
		}

		/// <summary>
		/// Gets the inner reader.
		/// </summary>
		/// <value>The inner reader.</value>
		public OracleDataReader InnerReader
		{
			get { return this.innerReader; }
		}
		#endregion

		#region Construction
		/// <summary>
		/// Initializes a new instance of the <see cref="OracleDataReaderWrapper"/> class.
		/// </summary>
		/// <param name="reader">The reader.</param>
		internal OracleDataReaderWrapper(OracleDataReader reader)
		{
			this.innerReader = reader;
		}
        #endregion

        #region Operators
        /// <summary>
        /// Performs an explicit conversion from <see cref="EntLibContrib.Data.ManagedOdpNet.OracleDataReaderWrapper"/> to <see cref="Oracle.ManagedDataAccess.Client.OracleDataReader"/>.
        /// </summary>
        /// <param name="oracleDataReaderWrapper">The oracle data reader wrapper.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator OracleDataReader(OracleDataReaderWrapper oracleDataReaderWrapper)
		{
			return oracleDataReaderWrapper.InnerReader;
		}
		#endregion

		#region Indexers
		/// <summary>
		/// Gets the <see cref="System.Object"/> with the specified i.
		/// </summary>
		/// <value></value>
		public object this[int i]
		{
			get { return InnerReader[i]; }
		}

		/// <summary>
		/// Gets the <see cref="System.Object"/> with the specified name.
		/// </summary>
		/// <value></value>
		public object this[string name]
		{
			get { return InnerReader[name]; }
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Converts the oracle data reader wrapper to an oracle data reader.
		/// </summary>
		/// <param name="oracleDataReaderWrapper">The oracle data reader wrapper.</param>
		/// <returns></returns>
		public static OracleDataReader ToOracleDataReader(OracleDataReaderWrapper oracleDataReaderWrapper)
		{
			return oracleDataReaderWrapper.InnerReader;
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return InnerReader.GetEnumerator();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (InnerReader != null)
				{
					InnerReader.Dispose();
				}
			}
		}

		/// <summary>
		/// Closes the <see cref="T:System.Data.IDataReader"/> Object.
		/// </summary>
		public void Close()
		{
			InnerReader.Close();
		}

		/// <summary>
		/// Returns a <see cref="T:System.Data.DataTable"/> that describes the column metadata of the <see cref="T:System.Data.IDataReader"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Data.DataTable"/> that describes the column metadata.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">
		/// The <see cref="T:System.Data.IDataReader"/> is closed.
		/// </exception>
		public DataTable GetSchemaTable()
		{
			return InnerReader.GetSchemaTable();
		}

		/// <summary>
		/// Advances the data reader to the next result, when reading the results of batch SQL statements.
		/// </summary>
		/// <returns>
		/// true if there are more rows; otherwise, false.
		/// </returns>
		public bool NextResult()
		{
			return InnerReader.NextResult();
		}

		/// <summary>
		/// Advances the <see cref="T:System.Data.IDataReader"/> to the next record.
		/// </summary>
		/// <returns>
		/// true if there are more rows; otherwise, false.
		/// </returns>
		public bool Read()
		{
			return InnerReader.Read();
		}

		/// <summary>
		/// Gets the boolean.
		/// </summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <remarks>
		/// Bit data type is mapped to a number in Oracle database. When reading bit data from Oracle database,
		/// it will map to 0 as false and everything else as true.  This method uses System.Convert.ToBoolean() method
		/// for type conversions.
		/// </remarks>
		public bool GetBoolean(int i)
		{
			return Convert.ToBoolean(InnerReader[i], CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Gets the byte.
		/// </summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>
		/// The 8-bit unsigned integer value of the specified column.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public byte GetByte(int i)
		{
			return Convert.ToByte(InnerReader[i], CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.
		/// </summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <returns>The actual number of bytes read.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return InnerReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		/// <summary>
		/// Gets the character value of the specified column.
		/// </summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>
		/// The character value of the specified column.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public Char GetChar(int i)
		{
			return InnerReader.GetChar(i);
		}

		/// <summary>
		/// Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.
		/// </summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <returns>The actual number of characters read.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return InnerReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		/// <summary>
		/// Returns an <see cref="T:System.Data.IDataReader"/> for the specified column ordinal.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// An <see cref="T:System.Data.IDataReader"/>.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public IDataReader GetData(int i)
		{
			return InnerReader.GetData(i);
		}

		/// <summary>
		/// Gets the data type information for the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The data type information for the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public string GetDataTypeName(int i)
		{
			return InnerReader.GetDataTypeName(i);
		}

		/// <summary>
		/// Gets the date and time data value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The date and time data value of the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public DateTime GetDateTime(int i)
		{
			return InnerReader.GetDateTime(i);
		}

		/// <summary>
		/// Gets the fixed-position numeric value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The fixed-position numeric value of the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public decimal GetDecimal(int i)
		{
			return InnerReader.GetDecimal(i);
		}

		/// <summary>
		/// Gets the double-precision floating point number of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The double-precision floating point number of the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public double GetDouble(int i)
		{
			return InnerReader.GetDouble(i);
		}

		/// <summary>
		/// Gets the <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public Type GetFieldType(int i)
		{
			return InnerReader.GetFieldType(i);
		}

		/// <summary>
		/// Gets the single-precision floating point number of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The single-precision floating point number of the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public float GetFloat(int i)
		{
			return InnerReader.GetFloat(i);
		}

		/// <summary>
		/// Returns the GUID value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The GUID value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public Guid GetGuid(int i)
		{
			byte[] guidBuffer = (byte[])InnerReader[i];
			return new Guid(guidBuffer);
		}

		/// <summary>
		/// Gets the 16-bit signed integer value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The 16-bit signed integer value of the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public short GetInt16(int i)
		{
			return Convert.ToInt16(InnerReader[i], CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Gets the 32-bit signed integer value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The 32-bit signed integer value of the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public int GetInt32(int i)
		{
			return InnerReader.GetInt32(i);
		}

		/// <summary>
		/// Gets the 64-bit signed integer value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The 64-bit signed integer value of the specified field.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public long GetInt64(int i)
		{
			return InnerReader.GetInt64(i);
		}

		/// <summary>
		/// Gets the name for the field to find.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The name of the field or the empty string (""), if there is no value to return.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public string GetName(int i)
		{
			return InnerReader.GetName(i);
		}

		/// <summary>
		/// Return the index of the named field.
		/// </summary>
		/// <param name="name">The name of the field to find.</param>
		/// <returns>The index of the named field.</returns>
		public int GetOrdinal(string name)
		{
			return InnerReader.GetOrdinal(name);
		}

		/// <summary>
		/// Gets the string value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>The string value of the specified field.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public string GetString(int i)
		{
			return InnerReader.GetString(i);
		}

		/// <summary>
		/// Return the value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// The <see cref="T:System.Object"/> which will contain the field value upon return.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public object GetValue(int i)
		{
			return InnerReader.GetValue(i);
		}

		/// <summary>
		/// Gets all the attribute fields in the collection for the current record.
		/// </summary>
		/// <param name="values">An array of <see cref="T:System.Object"/> to copy the attribute fields into.</param>
		/// <returns>
		/// The number of instances of <see cref="T:System.Object"/> in the array.
		/// </returns>
		public int GetValues(object[] values)
		{
			return InnerReader.GetValues(values);
		}

		/// <summary>
		/// Return whether the specified field is set to null.
		/// </summary>
		/// <param name="i">The index of the field to find.</param>
		/// <returns>
		/// true if the specified field is set to null; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		/// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
		/// </exception>
		public bool IsDBNull(int i)
		{
			return InnerReader.IsDBNull(i);
		}
		#endregion
	}
}