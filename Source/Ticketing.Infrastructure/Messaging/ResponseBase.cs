using System;
using System.Runtime.Serialization;

namespace Ticketing.Infrastructure.Messaging
{
	/// <summary>
	/// Base response class for Messaging pattern.
	/// </summary>
	[DataContract(IsReference = true)]
	[KnownType(typeof (ResponseBase<RequestBase>))]
	public abstract class ResponseBase<T> : IResponse where T : IRequest
	{
		/// <summary>
		/// Unique identifier of the response. 
		/// </summary>
		[DataMember]
		public Guid ResponseToken { get; set; }

		/// <summary>
		/// Response result. True if request was successfull. 
		/// If False, Client should expect some exception explanation in Message property of the response.
		/// </summary>
		[DataMember]
		public bool Success { get; set; }

		/// <summary>
		/// Text message used ti describe exception that occured while executing request.
		/// Property should be null if Success property is True.
		/// </summary>
		[DataMember]
		public string Message { get; set; }

		/// <summary>
		/// Request that invoket this response.
		/// </summary>
		[DataMember]
		public T Request { get; set; }

		/// <summary>
		/// Exception that occured while executing request.
		/// Property should be null if Success property is True.
		/// </summary>
		public Exception Exception { get; set; }
	}
}