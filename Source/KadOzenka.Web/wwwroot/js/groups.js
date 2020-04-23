function MapTourGroups(response) {
	return response.map(function (mainGroup) {
		return {
			Id: mainGroup.Id,
			GroupName: mainGroup.GroupName,
			UrlForEdit: mainGroup.UrlForEdit,
			GroupType: mainGroup.GroupType,
			items: mainGroup.Items.map(function (group) {
				return {
					Id: group.Id,
					GroupName: group.GroupName,
					UrlForEdit: group.UrlForEdit,
					GroupType: group.GroupType,
					items: group.Items.map(function (subgroup) {
						return {
							Id: subgroup.Id,
							GroupName: subgroup.GroupName,
							UrlForEdit: subgroup.UrlForEdit,
							GroupType: subgroup.GroupType
						}
					})
				}
			})
		}
	});
}