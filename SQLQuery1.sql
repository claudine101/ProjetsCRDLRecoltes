select * from client c join recolte r
on c.ID_client=r.ID_client
join station_lavage s
on r.ID_station=s.ID_station
join employe_station_lavage e 
on e.ID_station=s.ID_station
where e.ID_employ=2
