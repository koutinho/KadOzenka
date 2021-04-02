using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectList
	{
		public List<xmlObjectBuild> Buildings;
		public List<xmlObjectConstruction> Constructions;
		public List<xmlObjectUncomplited> Uncompliteds;
		public List<xmlObjectFlat> Flats;
		public List<xmlObjectCarPlace> CarPlaces;
		public List<xmlObjectParcel> Parcels;
		public object myLock;

		public xmlObjectList()
		{
			Buildings = new List<xmlObjectBuild>();
			Constructions = new List<xmlObjectConstruction>();
			Uncompliteds = new List<xmlObjectUncomplited>();
			Flats = new List<xmlObjectFlat>();
			CarPlaces = new List<xmlObjectCarPlace>();
			Parcels = new List<xmlObjectParcel>();
			myLock = new object();
		}

		public void Add(xmlObject obj)
		{
			lock (myLock)
			{
				switch (obj.TypeObject)
				{
					case enTypeObject.toBuilding:
						Buildings.Add(new xmlObjectBuild(obj));
						break;
					case enTypeObject.toConstruction:
						Constructions.Add(new xmlObjectConstruction(obj));
						break;
					case enTypeObject.toFlat:
						Flats.Add(new xmlObjectFlat(obj));
						break;
					case enTypeObject.toCarPlace:
						CarPlaces.Add(new xmlObjectCarPlace(obj));
						break;
					case enTypeObject.toUncomplited:
						Uncompliteds.Add(new xmlObjectUncomplited(obj));
						break;
					case enTypeObject.toParcel:
						Parcels.Add(new xmlObjectParcel(obj));
						break;
					default:
						break;
				}
			};
		}

		public void Add(xmlObjectList objs)
		{
			Buildings.AddRange(objs.Buildings);
			Constructions.AddRange(objs.Constructions);
			Uncompliteds.AddRange(objs.Uncompliteds);
			Flats.AddRange(objs.Flats);
			CarPlaces.AddRange(objs.CarPlaces);
			Parcels.AddRange(objs.Parcels);
		}
		public void Clear()
		{
			Buildings.ForEach(x => x = null);
			Buildings.Clear();
			Constructions.ForEach(x => x = null);
			Constructions.Clear();
			Uncompliteds.ForEach(x => x = null);
			Uncompliteds.Clear();
			Flats.ForEach(x => x = null);
			Flats.Clear();
			CarPlaces.ForEach(x => x = null);
			CarPlaces.Clear();
			Parcels.ForEach(x => x = null);
			Parcels.Clear();
		}
	}
}