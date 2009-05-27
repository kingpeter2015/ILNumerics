using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using ILNumerics.Drawing.Collections; 
using ILNumerics.Drawing.Shapes; 
using ILNumerics.Drawing.Controls; 

namespace ILNumerics.Drawing.Graphs {
    /// <summary>
    /// Base class for all scene graph nodes. A node
    /// can contain an arbitrary number of other nodes, building 
    /// the graph. 
    /// </summary>
    public abstract class ILSceneGraphNode : 
                ICollection<ILSceneGraphNode>, IEnumerable<ILSceneGraphNode>, 
                IEnumerable {
        
        #region attributes 
        protected ILSceneGraphNode m_parent; 
        protected ILPoint3Df m_center;
        protected ILPoint3Df m_positionMin; 
        protected ILPoint3Df m_positionMax; 
        protected List<ILSceneGraphNode> m_childs = new List<ILSceneGraphNode>(); 
        protected ILArray<float> m_centers;
        protected ILPanel m_panel;
        protected bool m_invalidated; 
        #endregion

        #region eventing 
        public event EventHandler SizeChanged;
        protected virtual void OnSizeChanged() {
            if (SizeChanged != null)
                SizeChanged(this, new EventArgs());
        }
        public event EventHandler Invalidated;
        protected virtual void OnInvalidated() {
            if (Invalidated != null)
                Invalidated(this, new EventArgs());
        }
        public event EventHandler Changed;
        protected virtual void OnChanged(object sender, EventArgs args) {
            if (Changed != null)
                Changed(this, new EventArgs());
        }
        #endregion

        #region properties

        public ILSceneGraphNode Parent {
            get {
                return m_parent; 
            }
            set {
                m_parent = value; 
            }
        }
        public virtual ILPoint3Df PositionMin() {
            if (m_positionMin.IsEmtpy()) {
                m_positionMin = ILPoint3Df.MaxValue; 
                foreach (ILSceneGraphNode node in m_childs) {
                    m_positionMin = ILPoint3Df.Min(node.PositionMin(),m_positionMin); 
                }
            } 
            return m_positionMin;
        }
        public virtual ILPoint3Df PositionMax () {
            if (m_positionMax.IsEmtpy()) {
                m_positionMax = ILPoint3Df.MinValue; 
                foreach (ILSceneGraphNode node in m_childs) {
                    m_positionMax = ILPoint3Df.Max(node.PositionMax(),m_positionMax); 
                }
            }
            return m_positionMax; 
        }
        public virtual ILPoint3Df Center {
            get {
                if (m_center.IsEmtpy()) {
                    m_center = new ILPoint3Df();
                    for (int i = 0; i < m_childs.Count; i++) {
                        ILPoint3Df center = m_childs[i].Center;
                        m_center += center;
                        m_centers.SetValue(center.X, i, 0);
                        m_centers.SetValue(center.Y, i, 1);
                        m_centers.SetValue(center.Z, i, 2);
                    }
                    m_center /= (m_childs.Count);
                }
                return m_center;
            }
        }
        #endregion

        #region constructor
        public ILSceneGraphNode (ILPanel panel) {
            m_centers = new ILArray<float>(0,3); 
            m_panel = panel; 
            m_center = ILPoint3Df.Empty; 
            m_positionMin = ILPoint3Df.Empty; 
            m_positionMax = ILPoint3Df.Empty; 
            m_invalidated = true; 
        }
        #endregion

        #region public interface 
        public void Invalidate(bool invalidChilds) {
            m_center = ILPoint3Df.Empty;
            m_positionMin = ILPoint3Df.Empty;
            m_positionMax = ILPoint3Df.Empty;
            if (invalidChilds) {
                invalidateChilds(this);
            }
            if (Parent != null) {
                Parent.Invalidate(false);
            } else {
                OnInvalidated();
            }
            m_invalidated = true;
        }

        public void Invalidate() {
            Invalidate(true);
        }
        public virtual void Configure() {
            if (m_invalidated) {
                bool sizechanged = false; 
                ILPoint3Df oldPoint = m_positionMin; 
                m_positionMin = PositionMin();
                if (oldPoint != m_positionMin) sizechanged = true; 
                oldPoint = m_positionMax; 
                m_positionMax = PositionMax();
                if (!sizechanged && oldPoint != m_positionMax) sizechanged = true;
                m_center = Center;
                m_invalidated = false;
                if (sizechanged) 
                    OnSizeChanged(); 
            }
        }
        public virtual void Draw(ILRenderProperties props) { 
            if (m_childs != null && m_childs.Count > 0) {
                ILArray<int> indices = Computation.GetSortedIndices(
                                     m_centers,m_panel.Camera.Position); 
                foreach (int i in indices.Values) {
                    m_childs[i].Draw(props); 
                }
            }
        }
        #endregion

        #region private helper 
        protected void invalidateChilds(ILSceneGraphNode parent) {
            parent.m_center = ILPoint3Df.Empty;
            parent.m_positionMin = ILPoint3Df.Empty;
            parent.m_positionMax = ILPoint3Df.Empty;
            foreach (ILSceneGraphNode child in parent.m_childs) {
                child.invalidateChilds(child);
            }
        }
        #endregion

        #region IList<ILSceneGraphNode> Member

        public virtual int IndexOf(ILSceneGraphNode item) {
            return m_childs.IndexOf(item); 
        }

        public virtual void Insert(int index, ILSceneGraphNode item) {
            m_childs.Insert(index,item); 
        }

        public virtual void RemoveAt(int index) {
            m_childs.RemoveAt(index); 
        }
        #endregion

        #region ICollection<ILSceneGraphNode> Member

        public virtual void Add(ILSceneGraphNode item) {
            m_childs.Add(item);
            item.Parent = this; 
        }

        public virtual void Clear() {
            m_childs.Clear(); 
        }

        public bool Contains(ILSceneGraphNode item) {
            return m_childs.Contains(item); 
        }

        public void CopyTo(ILSceneGraphNode[] array, int arrayIndex) {
            m_childs.CopyTo(array,arrayIndex); 
        }

        public int Count {
            get { return m_childs.Count;  }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public virtual bool Remove(ILSceneGraphNode item) {
            return m_childs.Remove(item); 
        }

        #endregion

        #region IEnumerable<ILSceneGraphNode> Member

        public IEnumerator<ILSceneGraphNode> GetEnumerator() {
            return m_childs.GetEnumerator(); 
        }

        #endregion

        #region IEnumerable Member

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            foreach (ILSceneGraphNode node in m_childs) {
                yield return node; 
            }
        }

        #endregion

    private class Computation : ILNumerics.BuiltInFunctions.ILMath {
        /// <summary>
        /// compute distance to camera and return sorted indices for rendering
        /// </summary>
        /// <param name="centers">current primitive centers</param>
        /// <param name="position">current camera position</param>
        /// <returns>sorted indices of primitives in descending order</returns>
        internal static ILArray<int> GetSortedIndices(ILArray<float> centers, 
                                        ILPoint3Df position) {
            ILArray<float> pos = new float[]{ -position.X, -position.Y, -position.Z }; 
            // move camera outside of centers
            pos *= maxall(abs(centers)); 
            pos = repmat(pos,centers.Dimensions[0],1); 
            // compute distances
            ILArray<float> dist = sum(pow(centers-pos,2),1); 
            ILArray<double> ret; 
            sort(dist,out ret, 0,false); 
            return toint32(ret); 
        }
    }

    
    }

}