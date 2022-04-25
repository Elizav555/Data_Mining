from importlib.resources import path
from unicodedata import numeric
import matplotlib.pyplot as plt
import networkx as nx
import pandas as pd
import numpy as np
from scipy.spatial.distance import pdist
from mst_clustering import MSTClustering


data = np.genfromtxt(r'D:\Data_Mining\Clustering\emplSat.csv',  dtype=str,  delimiter=',',invalid_raise=False,encoding='utf-8', skip_header=1 ,loose=True)
#names =  np.genfromtxt(path,  dtype=str,  delimiter=',',invalid_raise=False,encoding='utf-8',usecols=(0) )
data_numeric = np.asarray(data[:,[2,12]],dtype=int)
df_salary = pd.DataFrame(data_numeric).values
norm_salary = (df_salary - df_salary.mean(axis=0)) / df_salary.std(axis=0)
# model = MSTClustering(cutoff_scale=1, approximate=False)
# labels = model.fit_predict(data_float)
# plt.scatter(data_float[:, 0],data_float[:, 1], c=labels, cmap='rainbow')
# plt.show()
import scipy.cluster.hierarchy as shc
from sklearn.cluster import AgglomerativeClustering
plt.title("Data Dendograms")
dend = shc.dendrogram(shc.linkage(norm_salary, method='ward'))
plt.show()
cluster = AgglomerativeClustering(n_clusters=5, affinity='euclidean', linkage='ward')
cluster.fit_predict(norm_salary)
plt.scatter(norm_salary[:,0], norm_salary[:,1], c=cluster.labels_, cmap='rainbow')
plt.show()